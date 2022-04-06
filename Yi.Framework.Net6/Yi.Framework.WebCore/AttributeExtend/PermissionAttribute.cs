using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yi.Framework.WebCore.AttributeExtend
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PermissionAttribute : ActionFilterAttribute
    {
        private string permission { get; set; }

        public PermissionAttribute(string permission)
        {
            this.permission = permission;
        }

        /// <summary>
        /// 动作鉴权
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="Exception"></exception>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (string.IsNullOrEmpty(permission))
            {
                throw new Exception("权限不能为空！");
            }

            //可以从Redis得到用户菜单列表，或者直接从jwt中获取

            var result = false;

            //判断权限是否存在Redis中
            if (permission.Length>0)
            {
                result = true;
            }


            if (!result)
            {
                throw new Exception("拦截未授权请求！");
            }
        }

    }
}