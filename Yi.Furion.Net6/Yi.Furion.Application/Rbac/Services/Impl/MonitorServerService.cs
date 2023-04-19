using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Furion.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Yi.Framework.Infrastructure.Extensions;
using Yi.Framework.Infrastructure.Helper;

namespace Yi.Furion.Application.Rbac.Services
{

    public class MonitorServerService: IMonitorServerService,IDynamicApiController, ITransient
    {
        private IWebHostEnvironment _hostEnvironment;
        private IHttpContextAccessor _httpContextAccessor;
        public MonitorServerService(IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this._hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("info")]
        public object GetInfo()
        {
            int cpuNum = Environment.ProcessorCount;
            string computerName = Environment.MachineName;
            string osName = RuntimeInformation.OSDescription;
            string osArch = RuntimeInformation.OSArchitecture.ToString();
            string version = RuntimeInformation.FrameworkDescription;
            string appRAM = ((double)Process.GetCurrentProcess().WorkingSet64 / 1048576).ToString("N2") + " MB";
            string startTime = Process.GetCurrentProcess().StartTime.ToString("yyyy-MM-dd HH:mm:ss");
            string sysRunTime = ComputerHelper.GetRunTime();
            string serverIP = _httpContextAccessor.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString() + ":" + _httpContextAccessor.HttpContext.Connection.LocalPort;//获取服务器IP

            var programStartTime = Process.GetCurrentProcess().StartTime;
            string programRunTime = DateTimeHelper.FormatTime((DateTime.Now - programStartTime).TotalMilliseconds.ToString().Split('.')[0].ParseToLong());
            var data = new
            {
                cpu = ComputerHelper.GetComputerInfo(),
                disk = ComputerHelper.GetDiskInfos(),
                sys = new { cpuNum, computerName, osName, osArch, serverIP, runTime = sysRunTime },
                app = new
                {
                    name = _hostEnvironment.EnvironmentName,
                    rootPath = _hostEnvironment.ContentRootPath,
                    webRootPath = _hostEnvironment.WebRootPath,
                    version,
                    appRAM,
                    startTime,
                    runTime = programRunTime,
                    host = serverIP
                },
            };

            return data;
        }

    }
}
