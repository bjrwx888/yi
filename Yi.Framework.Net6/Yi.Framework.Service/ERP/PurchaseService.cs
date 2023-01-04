using AutoMapper;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.DtoModel.ERP.Purchase;
using Yi.Framework.Interface.ERP;
using Yi.Framework.Model.ERP.Entitys;
using Yi.Framework.Repository;
using Yi.Framework.Service.Base.Crud;

namespace Yi.Framework.Service.ERP
{
    public class PurchaseService : CrudAppService<PurchaseEntity, PurchaseGetListOutput, long, PurchaseCreateInput, PurchaseUpdateInput>, IPurchaseService
    {
        private readonly ISugarUnitOfWork<UnitOfWork> _unitOfWork;
        private readonly IPurchaseDetailsService _purchaseDetailsService;
        public PurchaseService(IRepository<PurchaseEntity> repository, IMapper mapper, ISugarUnitOfWork<UnitOfWork> unitOfWork,
            IPurchaseDetailsService purchaseDetailsService) : base(repository, mapper)
        {
            _unitOfWork = unitOfWork;
            _purchaseDetailsService = purchaseDetailsService;
        }
        public async Task<PageModel<List<PurchaseGetListOutput>>> PageListAsync(PurchaseGetListInput input, PageParModel page)
        {
            RefAsync<int> totalNumber = 0;
            var data = await Repository._DbQueryable
                //.WhereIF(input.Code is not null,u=>u.Code.Contains(input.Code))
                //.WhereIF(input.Name is not null, u => u.Name.Contains(input.Name))
                .ToPageListAsync(page.PageNum, page.PageSize, totalNumber);
            return new PageModel<List<PurchaseGetListOutput>> { Total = totalNumber.Value, Data = await MapToGetListOutputDtosAsync(data) };
        }

        public override async Task<PurchaseGetListOutput> CreateAsync(PurchaseCreateInput input)
        {

            using (var uow = _unitOfWork.CreateContext())
            {
                var entity = await MapToEntityAsync(input);
                entity.PaidMoney = 0;
                entity.TotalMoney = input.PurchaseDetails.Sum(u => u.UnitPrice * u.TotalNumber);
                entity.PurchaseState = PurchaseStateEnum.Build;
                TryToSetTenantId(entity);
                var purchaseId = await Repository.InsertReturnSnowflakeIdAsync(entity);

                await _purchaseDetailsService.CreateAsync(input.PurchaseDetails);
            }

            return null;
        }
    }
}
