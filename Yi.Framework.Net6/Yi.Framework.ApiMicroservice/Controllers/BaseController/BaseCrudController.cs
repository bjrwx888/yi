using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Yi.Framework.Common.Models;
using Yi.Framework.Interface;
using Yi.Framework.Language;
using Yi.Framework.Model.Models;
using Yi.Framework.Model.Query;
using Yi.Framework.Repository;
using Yi.Framework.WebCore.AttributeExtend;

namespace Yi.Framework.ApiMicroservice.Controllers
{
    /// <summary>
    /// 6666
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseCrudController<T> : ControllerBase where T : BaseModelEntity,new()
    {
        public readonly ILogger<T> _logger;
        public IBaseService<T> _baseService;
        public IRepository<T> _repository;
        public BaseCrudController(ILogger<T> logger, IBaseService<T> iBaseService)
        {
            _logger = logger;
            _baseService = iBaseService;
            _repository = iBaseService._repository;
        }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Permission($"{nameof(T)}:get:one")]
        [HttpGet]
        public async Task<Result> Get(long id)
        {
            return Result.Success().SetData(await _repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 列表查询
        /// </summary>
        /// <returns></returns>
        [Permission($"{nameof(T)}:get:list")]
        [HttpPost]
        public async Task<Result> GetList(QueryCondition queryCondition)
        {
            return Result.Success().SetData(await _repository.GetListAsync(queryCondition));
        }

        /// <summary>
        /// 条件分页查询
        /// </summary>
        /// <param name="queryCondition"></param>
        /// <returns></returns>
        [Permission($"{nameof(T)}:get:page")]
        [HttpPost]
        public async  Task<Result> Page(QueryPageCondition queryCondition)
        {
            return Result.Success().SetData(await _repository.CommonPageAsync(queryCondition));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [Permission($"{nameof(T)}:add")]
        [HttpPost]
        public async Task<Result> Add(T entity)
        {
            return Result.Success().SetData(await _repository.InsertReturnSnowflakeIdAsync(entity));
        }
        
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [Permission($"{nameof(T)}:update")]
        [HttpPut]
        public async Task<Result> Update(T entity)
        {
            return Result.Success().SetStatus(await _repository.UpdateIgnoreNullAsync(entity));
        }

        /// <summary>
        /// 列表删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Permission($"{nameof(T)}:delete:list")]
        [HttpDelete]
        public async Task<Result> DeleteList(List<long> ids)
        {
            return Result.Success().SetStatus(await _repository.DeleteByLogicAsync(ids));
        }
    }
}
