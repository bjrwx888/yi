using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion;
using Furion.DataValidation;
using Furion.FriendlyException;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Yi.Furion.Rbac.Web.Core
{
    [UnifyModel(typeof(YiRESTfulResult<>))]
    public class YiRESTfulResultProvider : IUnifyResultProvider
    {
        public IActionResult OnException(ExceptionContext context, ExceptionMetadata metadata)
        {
            return new JsonResult(new YiRESTfulResult<object>()
                           , UnifyContext.GetSerializerSettings(context)); // 当前行仅限 Furion 4.6.6+ 使用
        }

        public async Task OnResponseStatusCodes(HttpContext context, int statusCode, UnifyResultSettingsOptions unifyResultSettings = null)
        {
            UnifyContext.SetResponseStatusCodes(context, statusCode, unifyResultSettings);

            switch (statusCode)
            {
                // 处理 401 状态码
                case StatusCodes.Status401Unauthorized:
                    await context.Response.WriteAsJsonAsync(new YiRESTfulResult<object>()
                        , App.GetOptions<JsonOptions>()?.JsonSerializerOptions);
                    break;
                // 处理 403 状态码
                case StatusCodes.Status403Forbidden:
                    await context.Response.WriteAsJsonAsync(new YiRESTfulResult<object>()
                        , App.GetOptions<JsonOptions>()?.JsonSerializerOptions);
                    break;
                default: break;
            }
        }

        public IActionResult OnSucceeded(ActionExecutedContext context, object data)
        {
            return new JsonResult(new YiRESTfulResult<object>()
                             , UnifyContext.GetSerializerSettings(context)); // 当前行仅限 Furion 4.6.6+ 使用
        }

        public IActionResult OnValidateFailed(ActionExecutingContext context, ValidationMetadata metadata)
        {
            return new JsonResult(new YiRESTfulResult<object>()
                             , UnifyContext.GetSerializerSettings(context)); // 当前行仅限 Furion 4.6.6+ 使用
        }
    }
}
