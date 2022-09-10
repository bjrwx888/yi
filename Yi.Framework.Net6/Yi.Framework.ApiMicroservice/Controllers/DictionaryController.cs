using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yi.Framework.Common.Helper;
using Yi.Framework.Common.Models;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;
using Yi.Framework.Service;
using Yi.Framework.WebCore;
using Yi.Framework.WebCore.AttributeExtend;
using Yi.Framework.WebCore.AuthorizationPolicy;

namespace Yi.Framework.ApiMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DictionaryController 
    {
        private IDictionaryService _iDictionaryService;
        private IDictionaryInfoService _iDictionaryInfoService;
        public DictionaryController(ILogger<DictionaryEntity> logger, IDictionaryService iDictionaryService, IDictionaryInfoService iDictionaryInfoService)
        {
            _iDictionaryService = iDictionaryService;
            _iDictionaryInfoService = iDictionaryInfoService;
        }

        /// <summary>
        /// 动态条件分页查询
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> PageList([FromQuery] DictionaryEntity dic, [FromQuery] PageParModel page)
        {
            return Result.Success().SetData(await _iDictionaryService.SelctPageList(dic, page));
        }

        /// <summary>
        /// 添加字典表
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> Add(DictionaryEntity dic)
        {
            return Result.Success().SetData(await _iDictionaryService._repository.InsertReturnSnowflakeIdAsync(dic));
        }

        /// <summary>
        /// 根据字典id获取字典表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<Result> GetById(long id)
        {
            return Result.Success().SetData(await _iDictionaryService._repository.GetByIdAsync(id));
        }


        /// <summary>
        /// 获取全部字典表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> GetList()
        {
            return Result.Success().SetData(await _iDictionaryService._repository.GetListAsync());
        }


        /// <summary>
        /// id范围删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]

        public async Task<Result> DelList(List<long> ids)
        {
            return Result.Success().SetStatus(await _iDictionaryService._repository.DeleteByIdsAsync(ids.ToDynamicArray()));
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Result> Update(DictionaryEntity dic)
        {
            return Result.Success().SetStatus(await _iDictionaryService._repository.UpdateIgnoreNullAsync(dic));
        }
    }
}
