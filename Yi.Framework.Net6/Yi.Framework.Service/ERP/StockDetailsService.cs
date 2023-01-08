using AutoMapper;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.DtoModel.ERP.StockDetails;
using Yi.Framework.Interface.ERP;
using Yi.Framework.Model.ERP.Entitys;
using Yi.Framework.Repository;
using Yi.Framework.Service.Base.Crud;

namespace Yi.Framework.Service.ERP
{
    public class StockDetailsService : CrudAppService<StockDetailsEntity, StockDetailsGetListOutput, long, StockDetailsCreateUpdateInput>, IStockDetailsService
    {
        public async Task<PageModel<List<StockDetailsGetListOutput>>> PageListAsync(StockDetailsGetListInput input, PageParModel page)
        {
            RefAsync<int> totalNumber = 0;
            var data = await Repository._DbQueryable
                .Where(u => u.StockDetailsType == input.StockDetailsType)
                .WhereIF(page.StartTime is not null && page.EndTime is not null, u => u.StockDetailsTime >= page.StartTime && u.StockDetailsTime <= page.EndTime)
                .ToPageListAsync(page.PageNum, page.PageSize, totalNumber);
            return new PageModel<List<StockDetailsGetListOutput>> { Total = totalNumber.Value, Data = await MapToGetListOutputDtosAsync(data) };
        }
    }
}
