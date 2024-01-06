using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quartz.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Yi.Framework.Bbs.Application.Services.Authentication
{
    public class QQAuthService : IRemoteService, ITransientDependency
    {
        private HttpContext HttpContext { get; set; }
        private ILogger<QQAuthService> _logger;
        public QQAuthService(IHttpContextAccessor httpContextAccessor, ILogger<QQAuthService> logger)
        {
            _logger = logger;
            HttpContext = httpContextAccessor.HttpContext ?? throw new ApplicationException("未注册Http");
        }
        [HttpGet("/auth/qq")]
        public async Task AuthQQAsync()
        {
            var data = await HttpContext.AuthenticateAsync("QQ");
            _logger.LogError($"QQ回调信息:{Newtonsoft.Json.JsonConvert.SerializeObject(data)}");
            _logger.LogError($"QQ回调身份:{Newtonsoft.Json.JsonConvert.SerializeObject(data.Principal)}");
        }
    }
}
