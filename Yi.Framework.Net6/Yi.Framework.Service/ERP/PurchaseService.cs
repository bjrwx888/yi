using AutoMapper;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Common.Attribute;
using Yi.Framework.Common.Models;
using Yi.Framework.DtoModel.ERP.Purchase;
using Yi.Framework.DtoModel.ERP.PurchaseDetails;
using Yi.Framework.Interface.ERP;
using Yi.Framework.Model.Base;
using Yi.Framework.Model.ERP.Entitys;
using Yi.Framework.Repository;
using Yi.Framework.Service.Base.Crud;

namespace Yi.Framework.Service.ERP
{
    public class PurchaseService : CrudAppService<PurchaseEntity, PurchaseGetListOutput, long, PurchaseCreateInput, PurchaseUpdateInput>, IPurchaseService
    {
        private ISugarUnitOfWork<UnitOfWork> _unitOfWork;
        private readonly IPurchaseDetailsService _purchaseDetailsService;
        public PurchaseService(ISugarUnitOfWork<UnitOfWork> unitOfWork,
            IPurchaseDetailsService purchaseDetailsService)
        {
            _unitOfWork = unitOfWork;
            _purchaseDetailsService = purchaseDetailsService;
        }
        public async Task<PageModel<List<PurchaseGetListOutput>>> PageListAsync(PurchaseGetListInput input, PageParModel page)
        {
            RefAsync<int> totalNumber = 0;
            var data = await Repository._DbQueryable
                .LeftJoin<SupplierEntity>((p, s) => p.SupplierId == s.Id)
                .WhereIF(input.Code is not null, u => u.Code.Contains(input.Code))
                .Select((p, s) =>  new PurchaseGetListOutput { SupplierName = s.Name },true)
                .ToPageListAsync(page.PageNum, page.PageSize, totalNumber);
            return new PageModel<List<PurchaseGetListOutput>> { Total = totalNumber.Value, Data = data };
        }

        public override async Task<PurchaseGetListOutput> CreateAsync(PurchaseCreateInput input)
        {
            PurchaseEntity entity = null;
            using (var uow = _unitOfWork.CreateContext())
            {
                entity = await MapToEntityAsync(input);
                entity.PaidMoney = 0;
                entity.TotalMoney = input.PurchaseDetails.Sum(u => u.UnitPrice * u.TotalNumber);
                entity.PurchaseState = PurchaseStateEnum.Build;
                TryToSetTenantId(entity);
                var purchaseId = await Repository.InsertReturnSnowflakeIdAsync(entity);
                entity.Id = purchaseId;


                await _purchaseDetailsService.CreateAsync(input.PurchaseDetails.SetPurchaseId(purchaseId));
                uow.Commit();
            }
            return await MapToGetListOutputDtoAsync(entity); ;
        }
    }
}
