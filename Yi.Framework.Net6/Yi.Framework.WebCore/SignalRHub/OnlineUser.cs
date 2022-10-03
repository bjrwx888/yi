using System;
using System.Collections.Generic;
using System.Text;

namespace Yi.Framework.WebCore.SignalRHub
{
    public class OnlineUser
    {
        /// <summary>
        /// 客户端连接Id
        /// </summary>
        public string ConnnectionId { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public long? Userid { get; set; }
        public string Name { get; set; }
        public DateTime LoginTime { get; set; }
        public string UserIP { get; set; }
        public string Location { get; set; }

        public OnlineUser(string clientid, string name, long? userid, string userip)
        {
            ConnnectionId = clientid;
            Name = name;
            LoginTime = DateTime.Now;
            Userid = userid;
            UserIP = userip;
        }
    }
}
