using Microsoft.AspNetCore.Mvc;
using Yi.Framework.Common.Models;
using Yi.Framework.DtoModel.ERP.StockDetails;
using Yi.Framework.Interface.ERP;

namespace Yi.Framework.ApiMicroservice.Controllers.ERP
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StockDetailsController : ControllerBase
    {
        private readonly ILogger<StockDetailsController> _logger;
        private readonly IStockDetailsService _stockDetailsService;
        public StockDetailsController(ILogger<StockDetailsController> logger, IStockDetailsService stockDetailsService)
        {
            _logger = logger;
            _stockDetailsService = stockDetailsService;
        }

        /// <summary>
        /// 分页查
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> PageList([FromQuery] StockDetailsGetListInput input, [FromQuery] PageParModel page)
        {
            var result = await _stockDetailsService.PageListAsync(input, page);
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
            var result = await _stockDetailsService.GetByIdAsync(id);
            return Result.Success().SetData(result);
        }

        /// <summary>
        /// 删
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<Result> Del(List<long> ids)
        {
            await _stockDetailsService.DeleteAsync(ids);
            return Result.Success();
        }
    }
}
