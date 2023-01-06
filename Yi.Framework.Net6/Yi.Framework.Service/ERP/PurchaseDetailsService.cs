using AutoMapper;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.DtoModel.ERP.PurchaseDetails;
using Yi.Framework.Interface.ERP;
using Yi.Framework.Model.ERP.Entitys;
using Yi.Framework.Repository;
using Yi.Framework.Service.Base.Crud;

namespace Yi.Framework.Service.ERP
{
    public class PurchaseDetailsService : CrudAppService<PurchaseDetailsEntity, PurchaseDetailsGetListOutput, long, PurchaseDetailsCreateUpdateInput>, IPurchaseDetailsService
    {
        public async Task<List<PurchaseDetailsGetListOutput>> GetListByPurchaseIdAsync(long purchaseId)
        {
           var data= await Repository._DbQueryable.Where(u => u.PurchaseId == purchaseId).ToListAsync();
            return await MapToGetListOutputDtos(data);
        }

        public async Task<PageModel<List<PurchaseDetailsGetListOutput>>> PageListAsync(PurchaseDetailsGetListInput input, PageParModel page)
        {
            RefAsync<int> totalNumber = 0;
            var data = await Repository._DbQueryable

                .ToPageListAsync(page.PageNum, page.PageSize, totalNumber);
            return new PageModel<List<PurchaseDetailsGetListOutput>> { Total = totalNumber.Value, Data = await MapToGetListOutputDtosAsync(data) };
        }
    }
}
