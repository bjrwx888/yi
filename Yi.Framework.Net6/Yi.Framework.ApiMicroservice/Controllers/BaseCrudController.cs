using Microsoft.AspNetCore.Mvc;
using Yi.Framework.Common.Models;
using Yi.Framework.Model.Query;
using Yi.Framework.Repository;
using Yi.Framework.WebCore.AttributeExtend;

namespace Yi.Framework.ApiMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseCrudController<T> : ControllerBase where T : class,new()
    {
        private readonly ILogger<T> _logger;
        public IRepository<T> _iRepository;
        public BaseCrudController(ILogger<T> logger, IRepository<T> iRepository)
        {
            _logger = logger;
            _iRepository = iRepository;
        }
        [Permission($"{nameof(T)}:Get:One")]
        [HttpGet]
        public async Task<Result> Get(object id)
        {
            return Result.Success().SetData(await _iRepository.GetByIdAsync(id));
        }
        [Permission($"{nameof(T)}:Get:List")]
        [HttpGet]
        public async Task<Result> GetList()
        {
            return Result.Success().SetData(await _iRepository.GetListAsync());
        }
        [Permission($"{nameof(T)}:Get:Page")]
        [HttpPost]
        public async  Task<Result> Page(QueryCondition queryCondition)
        {
            return Result.Success().SetData(await _iRepository.CommonPage(queryCondition));
        }
        [Permission($"{nameof(T)}:Add")]
        [HttpPost]
        public async Task<Result> Add(T entity)
        {
            return Result.Success().SetData(await _iRepository.InsertReturnEntityAsync(entity));
        }
        [Permission($"{nameof(T)}:Update")]
        [HttpPut]
        public async Task<Result> Update(T entity)
        {
            return Result.Success().SetStatus(await _iRepository.UpdateAsync(entity));
        }
        [Permission($"{nameof(T)}:Delete:List")]
        [HttpDelete]
        public async Task<Result> DeleteList(object[] ids)
        {
            return Result.Success().SetStatus(await _iRepository.DeleteByIdsAsync(ids));
        }
    }
}
