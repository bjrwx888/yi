using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.DtoModel.ERP.StockDetails;
using Yi.Framework.Interface.Base.Crud;

namespace Yi.Framework.Interface.ERP
{
    public interface IStockDetailsService : ICrudAppService<StockDetailsGetListOutput, long, StockDetailsCreateUpdateInput>
    {
        Task<PageModel<List<StockDetailsGetListOutput>>> PageListAsync(StockDetailsGetListInput input, PageParModel page);
    }
}
