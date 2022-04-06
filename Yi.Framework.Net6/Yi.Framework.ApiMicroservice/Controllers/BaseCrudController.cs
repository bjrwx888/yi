using Microsoft.AspNetCore.Mvc;
using Yi.Framework.Common.Models;
using Yi.Framework.Model.Query;
using Yi.Framework.Repository;

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

        [HttpGet]
        public async Task<Result> Get()
        {
            return Result.Success().SetData(await _iRepository.GetListAsync());
        }

        [HttpPost]
        public async Task<Result> Page(QueryCondition queryCondition)
        {
            return Result.Success().SetData(_iRepository.CommonPage(queryCondition));
        }

        [HttpPost]
        public async Task<Result> Add(T entity)
        {
            return Result.Success().SetData(await _iRepository.InsertReturnEntityAsync(entity));
        }
        [HttpPut]
        public async Task<Result> Update(T entity)
        {
            return Result.Success().SetStatus(await _iRepository.UpdateAsync(entity));
        }
        [HttpDelete]
        public async Task<Result> Delete(object[] ids)
        {
            return Result.Success().SetStatus(await _iRepository.DeleteByIdsAsync(ids));
        }
    }
}
