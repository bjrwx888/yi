using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Yi.Framework.Auth.JwtBearer.Authorization;
using Yi.Framework.Core.Exceptions;

namespace SF.AspNetCore.Auth.Authorization;
public class PermissionGlobalAttribute : ActionFilterAttribute
{
    private  readonly IPermissionHandler _permissionHandler;
    public PermissionGlobalAttribute(IPermissionHandler permissionHandler)
    {
        _permissionHandler=permissionHandler;
    }
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionDescriptor is not ControllerActionDescriptor controllerActionDescriptor) return;
        PermissionAttribute? perAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
               .FirstOrDefault(a => a.GetType().Equals(typeof(PermissionAttribute))) as PermissionAttribute;
        //空对象直接返回
        if (perAttribute is null) return;

        var result = _permissionHandler.IsPass(perAttribute.Code);

        if (!result)
        {
            throw new AuthException(message: $"您无权限访问该接口-{context.HttpContext.Request.Path.Value}");
        }
    }
}
