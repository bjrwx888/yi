using System;
using IPTools.Core;
using SqlSugar;
using UAParser;
using Yi.Framework.Infrastructure.AspNetCore;
using Yi.Framework.Infrastructure.Data.Auditing;
using Yi.Framework.Infrastructure.Ddd.Entities;

namespace Yi.Furion.Core.Rbac.Entities
{
    [SugarTable("LoginLog")]
    public class LoginLogEntity : IEntity<long>, ICreationAuditedObject
    {
        [SugarColumn(ColumnName = "Id", IsPrimaryKey = true)]
        public long Id { get; set; }
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 登录用户 
        ///</summary>
        [SugarColumn(ColumnName = "LoginUser")]
        public string LoginUser { get; set; }
        /// <summary>
        /// 登录地点 
        ///</summary>
        [SugarColumn(ColumnName = "LoginLocation")]
        public string LoginLocation { get; set; }
        /// <summary>
        /// 登录Ip 
        ///</summary>
        [SugarColumn(ColumnName = "LoginIp")]
        public string LoginIp { get; set; }
        /// <summary>
        /// 浏览器 
        ///</summary>
        [SugarColumn(ColumnName = "Browser")]
        public string Browser { get; set; }
        /// <summary>
        /// 操作系统 
        ///</summary>
        [SugarColumn(ColumnName = "Os")]
        public string Os { get; set; }
        /// <summary>
        /// 登录信息 
        ///</summary>
        [SugarColumn(ColumnName = "LogMsg")]
        public string LogMsg { get; set; }

        public long? CreatorId { get; set; }
    }

    public static class LoginLogEntityExtensions { 
    
    

    /// <summary>
    /// 记录用户登陆信息
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static LoginLogEntity GetLoginLogInfo(this HttpContext context)
    {
        ClientInfo GetClientInfo(HttpContext context)
        {
            var str = context.GetUserAgent();
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(str);
            return c;
        }
        var ipAddr = context.GetClientIp();
        IpInfo location;
        if (ipAddr == "127.0.0.1")
        {
            location = new IpInfo() { Province = "本地", City = "本机" };
        }
        else
        {
            location = IpTool.Search(ipAddr);
        }
        ClientInfo clientInfo = GetClientInfo(context);
        LoginLogEntity entity = new()
        {
            Browser = clientInfo.Device.Family,
            Os = clientInfo.OS.ToString(),
            LoginIp = ipAddr,
            LoginLocation = location.Province + "-" + location.City
        };

        return entity;
    }
    }
}
