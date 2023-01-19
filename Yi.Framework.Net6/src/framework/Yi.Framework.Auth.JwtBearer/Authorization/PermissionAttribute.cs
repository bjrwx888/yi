using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.CurrentUsers;
using Yi.Framework.Core.Exceptions;
using Yi.Framework.Core.Model;

namespace Yi.Framework.Auth.JwtBearer.Authorization
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PermissionAttribute : ActionFilterAttribute
    {
        private string Permission { get; set; }

        public PermissionAttribute(string permission)
        {
            this.Permission = permission;
        }

        /// <summary>
        /// 动作鉴权
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="Exception"></exception>
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var permissionHandler = ServiceLocatorModel.Instance.GetRequiredService<IPermissionHandler>();

            var currentUser = ServiceLocatorModel.Instance.GetRequiredService<ICurrentUser>();



            var result = permissionHandler.IsPass(Permission, currentUser);

            if (!result)
            {
                throw new AuthException(message: $"您无权限访问该接口-{ context.HttpContext.Request.Path.Value}");
            }
        }

    }
}
