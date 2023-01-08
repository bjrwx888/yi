using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.DtoModel.ERP.Stock;
using Yi.Framework.Interface.Base.Crud;

namespace Yi.Framework.Interface.ERP
{
    public interface IStockService : ICrudAppService<StockGetListOutput, long, StockCreateUpdateInput>
    {
        Task<PageModel<List<StockGetListOutput>>> PageListAsync(StockGetListInput input, PageParModel page);

        /// <summary>
        /// Èë¿â
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StockGetListOutput> InputStockAsync(StockCreateUpdateInput input);

        /// <summary>
        /// ³ö¿â
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StockGetListOutput> OutputStockAsync(StockCreateUpdateInput input);
    }
}
