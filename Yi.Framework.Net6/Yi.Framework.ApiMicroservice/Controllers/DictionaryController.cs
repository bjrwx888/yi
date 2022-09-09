using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;
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
        public DictionaryController(ILogger<DictionaryEntity> logger, IDictionaryService iDictionaryService)
        {
            _iDictionaryService = iDictionaryService;
        }


        [HttpGet]
        public async Task<Result> PageList([FromQuery] DictionaryEntity dic, [FromQuery] PageParModel page)
        {
            return Result.Success().SetData(await _iDictionaryService.SelctPageList(dic, page));
        }

        [HttpGet]
        [Route("{type}")]
        public async Task<Result> GetListByType([FromRoute] string type)
        {
            return Result.Success().SetData(await _iDictionaryService._repository.GetListAsync(u=>u.DictType==type&&u.IsDeleted==false));
        }
    }
}
