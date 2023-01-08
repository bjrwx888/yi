using AutoMapper;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Common.Exceptions;
using Yi.Framework.Common.Models;
using Yi.Framework.DtoModel.ERP.Stock;
using Yi.Framework.Interface.ERP;
using Yi.Framework.Model.ERP.Entitys;
using Yi.Framework.Repository;
using Yi.Framework.Service.Base.Crud;

namespace Yi.Framework.Service.ERP
{
    public class StockService : CrudAppService<StockEntity, StockGetListOutput, long, StockCreateUpdateInput>, IStockService
    {
        private IRepository<StockDetailsEntity> _detailsRepository;
        private ISugarUnitOfWork<UnitOfWork> _unitOfWork;
        public StockService(IRepository<StockDetailsEntity> detailsRepository, ISugarUnitOfWork<UnitOfWork> unitOfWork)
        {
            _detailsRepository = detailsRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<PageModel<List<StockGetListOutput>>> PageListAsync(StockGetListInput input, PageParModel page)
        {
            RefAsync<int> totalNumber = 0;
            var data = await Repository._DbQueryable
                   .LeftJoin<WarehouseEntity>((stock, warehouse) => stock.WarehouseId == warehouse.Id)
                          .LeftJoin<MaterialEntity>((stock, warehouse, material) => stock.MaterialId == material.Id)
                          .Select((stock, warehouse, material) => new StockGetListOutput
                          {
                              MaterialName = material.Name,
                              WarehouseName = warehouse.Name,
                              UnitName = material.UnitName
                          }, true)
                .ToPageListAsync(page.PageNum, page.PageSize, totalNumber);
            return new PageModel<List<StockGetListOutput>> { Total = totalNumber.Value, Data = data };
        }

        public override async Task<StockGetListOutput> GetByIdAsync(long id)
        {
            return await Repository._DbQueryable
                     .LeftJoin<WarehouseEntity>((stock, warehouse) => stock.WarehouseId == warehouse.Id)
                            .LeftJoin<MaterialEntity>((stock, warehouse, material) => stock.MaterialId == material.Id)
                            .Select((stock, warehouse, material) => new StockGetListOutput
                            {
                                MaterialName = material.Name,
                                WarehouseName = warehouse.Name,
                                UnitName = material.UnitName
                            }, true)
                  .FirstAsync(stock => stock.Id==id);
        }





        /// <summary>
        /// 入库操作,需要工作单元事务操作
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StockGetListOutput> InputStockAsync(StockCreateUpdateInput input)
        {
            StockGetListOutput result = new();
            using (var uow = _unitOfWork.CreateContext())
            {
                var entity = await MapToEntityAsync(input);
                //先判断是否存在该物料，如果存在，直接添加数量
                var entityData = await Repository.GetFirstAsync(u => u.MaterialId == entity.MaterialId);
                if (entityData is not null)
                {
                    entityData.Number += input.Number;
                    await Repository.UpdateIgnoreNullAsync(entityData);
                }
                //添加一条库存数据
                else
                {
                    await Repository.InsertReturnSnowflakeIdAsync(entity);
                }

                result = await Repository._DbQueryable
                          .LeftJoin<WarehouseEntity>((stock, warehouse) => stock.WarehouseId == warehouse.Id)
                          .LeftJoin<MaterialEntity>((stock, warehouse, material) => stock.MaterialId == material.Id)
                          .Select((stock, warehouse, material) => new StockGetListOutput
                          {
                              MaterialName = material.Name,
                              WarehouseName = warehouse.Name,
                              UnitName = material.UnitName
                          }, true).FirstAsync();

                //还需要进行入库明细的插入
                StockDetailsEntity stockDetailsEntity = new();
                stockDetailsEntity.StockId = result.Id;
                stockDetailsEntity.WarehouseId = input.WarehouseId;
                stockDetailsEntity.MaterialId = input.MaterialId;
                stockDetailsEntity.WarehouseName = result.WarehouseName;
                stockDetailsEntity.MaterialName = result.MaterialName;
                stockDetailsEntity.StockDetailsTime = DateTime.Now;
                stockDetailsEntity.Quality = input.Quality;
                stockDetailsEntity.Number = input.Number;
                stockDetailsEntity.StockDetailsType = StockDetailsTypeEnum.Input;

                await _detailsRepository.InsertReturnSnowflakeIdAsync(stockDetailsEntity);

                uow.Commit();

            }

            return result;
        }

        /// <summary>
        /// 出库操作,需要工作单元事务操作
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StockGetListOutput> OutputStockAsync(StockCreateUpdateInput input)
        {
            StockGetListOutput result = new();
            using (var uow = _unitOfWork.CreateContext())
            {
                var entity = await Repository.GetByIdAsync(input.Id);

                if (entity is null)
                {
                    throw new ApplicationException("出库失败，该库存不存在");
                }


                //先判断是否存在该物料，如果存在，直接减去数量，如果减完数量=0，直接删除该条数据，如果小于零，库存不足
                var entityData = await Repository.GetFirstAsync(u => u.MaterialId == entity.MaterialId);
                if (entityData is not null)
                {
                    entityData.Number -= input.Number;

                    if (entityData.Number < 0)
                    {
                        throw new UserFriendlyException("库存不足");
                    }
                    else if (entityData.Number == 0)
                    {
                        await Repository.DeleteAsync(u => u.MaterialId == input.MaterialId);
                    }
                    else
                    {
                        await Repository.UpdateIgnoreNullAsync(entityData);
                    }

                }
                //不存在该物料
                else
                {
                    throw new UserFriendlyException("库存未发现该物料");
                }
                result = await Repository._DbQueryable
                          .LeftJoin<WarehouseEntity>((stock, warehouse) => stock.WarehouseId == warehouse.Id)
                          .LeftJoin<MaterialEntity>((stock, warehouse, material) => stock.MaterialId == material.Id)
                          .Select((stock, warehouse, material) => new StockGetListOutput
                          {
                              MaterialName = material.Name,
                              WarehouseName = warehouse.Name,
                              UnitName = material.UnitName
                          }, true).FirstAsync();

                //还需要进行入库明细的插入
                StockDetailsEntity stockDetailsEntity = new();
                stockDetailsEntity.StockId = result.Id;
                stockDetailsEntity.WarehouseId = input.WarehouseId;
                stockDetailsEntity.MaterialId = input.MaterialId;
                stockDetailsEntity.WarehouseName = result.WarehouseName;
                stockDetailsEntity.MaterialName = result.MaterialName;
                stockDetailsEntity.StockDetailsTime = DateTime.Now;
                stockDetailsEntity.Quality = input.Quality;
                stockDetailsEntity.Number = input.Number;
                stockDetailsEntity.StockDetailsType = StockDetailsTypeEnum.Output;
                await _detailsRepository.InsertReturnSnowflakeIdAsync(stockDetailsEntity);
                uow.Commit();
            }

            return result;
        }

    }
}
