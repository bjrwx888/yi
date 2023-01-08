using Microsoft.AspNetCore.Mvc;
using Yi.Framework.Common.Models;
using Yi.Framework.DtoModel.ERP.Stock;
using Yi.Framework.Interface.ERP;

namespace Yi.Framework.ApiMicroservice.Controllers.ERP
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StockController : ControllerBase
    {
        private readonly ILogger<StockController> _logger;
        private readonly IStockService _stockService;
        public StockController(ILogger<StockController> logger, IStockService stockService)
        {
            _logger = logger;
            _stockService = stockService;
        }

        /// <summary>
        /// 分页查
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> PageList([FromQuery] StockGetListInput input, [FromQuery] PageParModel page)
        {
            var result = await _stockService.PageListAsync(input, page);
            return Result.Success().SetData(result);
        }

        /// <summary>
        /// 单查
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<Result> GetById(long id)
        {
            var result = await _stockService.GetByIdAsync(id);
            return Result.Success().SetData(result);
        }


        /// <summary>
        /// 入库
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> InputStock(StockCreateUpdateInput input)
        {
            var result = await _stockService.InputStockAsync(input);
            return Result.Success().SetData(result);
        }

        /// <summary>
        /// 出库
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> OutputStock(StockCreateUpdateInput input)
        {
            var result = await _stockService.OutputStockAsync(input);
            return Result.Success().SetData(result);
        }
    }
}
