using Microsoft.AspNetCore.Mvc;
using Yi.Framework.Common.Models;
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
    public class BaseCrudController<T> : ControllerBase where T : class,new()
    {
        private readonly ILogger<T> _logger;
        public IRepository<T> _iRepository;
        /// <summary>
        /// jb
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="iRepository"></param>
        public BaseCrudController(ILogger<T> logger, IRepository<T> iRepository)
        {
            _logger = logger;
            _iRepository = iRepository;
        }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Permission($"{nameof(T)}:get:one")]
        [HttpGet]
        public async Task<Result> Get(object id)
        {
            return Result.Success().SetData(await _iRepository.GetByIdAsync(id));
        }

        /// <summary>
        /// 列表查询
        /// </summary>
        /// <returns></returns>
        [Permission($"{nameof(T)}:get:list")]
        [HttpGet]
        public async Task<Result> GetList()
        {
            return Result.Success().SetData(await _iRepository.GetListAsync());
        }

        /// <summary>
        /// 条件分页查询
        /// </summary>
        /// <param name="queryCondition"></param>
        /// <returns></returns>
        [Permission($"{nameof(T)}:get:page")]
        [HttpPost]
        public async  Task<Result> Page(QueryCondition queryCondition)
        {
            return Result.Success().SetData(await _iRepository.CommonPage(queryCondition));
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
            return Result.Success().SetData(await _iRepository.InsertReturnEntityAsync(entity));
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
            return Result.Success().SetStatus(await _iRepository.UpdateAsync(entity));
        }

        /// <summary>
        /// 列表删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Permission($"{nameof(T)}:delete:list")]
        [HttpDelete]
        public async Task<Result> DeleteList(object[] ids)
        {
            return Result.Success().SetStatus(await _iRepository.DeleteByIdsAsync(ids));
        }
    }
}
