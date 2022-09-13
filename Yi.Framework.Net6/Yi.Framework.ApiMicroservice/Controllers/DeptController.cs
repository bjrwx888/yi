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
    public class DeptController : BaseSimpleCrudController<DeptEntity>
    {
        private IDeptService _iDeptService;
        public DeptController(ILogger<DeptEntity> logger, IDeptService iDeptService) : base(logger, iDeptService)
        {
            _iDeptService = iDeptService;
        }

        /// <summary>
        /// 动态条件查询
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> SelctGetList([FromQuery] DeptEntity dept)
        {
            return Result.Success().SetData(await _iDeptService.SelctGetList(dept));
        }



        public override async Task<Result> Add(DeptEntity entity)
        {
            return await base.Add(entity);
        }

        public override async Task<Result> Update(DeptEntity entity)
        {
            return await base.Update(entity);
        }
    }
}
