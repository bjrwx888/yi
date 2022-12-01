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
    public class CommentController : BaseSimpleCrudController<CommentEntity>
    {
        private ICommentService _iCommentService;
        public CommentController(ILogger<CommentEntity> logger, ICommentService iCommentService) : base(logger, iCommentService)
        {
            _iCommentService = iCommentService;
        }

        /// <summary>
        /// 获取全部一级评论
        /// </summary>
        /// <returns></returns>
        public override async Task<Result> GetList()
        {
            var data = await _repository.GetListAsync(u=>u.UserId==null);
            return Result.Success().SetData(data);
        }

        /// <summary>
        /// 获取一级评论详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Task<Result> GetById([FromRoute] long id)
        {
            return base.GetById(id);
        }

        /// <summary>
        /// 回复文章或回复评论
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> Comment()
        { 
        
        }
    }
}
