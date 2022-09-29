using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.WebCore.AttributeExtend
{
    public class GlobalLogAttribute : ActionFilterAttribute
    {
        private ILogger<GlobalLogAttribute> _logger;
        //注入一个日志服务
        public GlobalLogAttribute(ILogger<GlobalLogAttribute> logger)
        {
            _logger = logger;
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            try
            {
                //查找标签，获取标签对象
                if (context.ActionDescriptor is not ControllerActionDescriptor controllerActionDescriptor) return;
                LogAttribute logAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
                  .FirstOrDefault(a => a.GetType().Equals(typeof(LogAttribute))) as LogAttribute;
                if (logAttribute == null) return;

                string controller = context.RouteData.Values["Controller"].ToString();
                string action = context.RouteData.Values["Action"].ToString();
                string ip = "127.0.0.1";
                string ipData = "深圳";
                //日志服务插入一条操作记录即可
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"操作日志错误:{ex.Message}");
            }

        }
    }
}
