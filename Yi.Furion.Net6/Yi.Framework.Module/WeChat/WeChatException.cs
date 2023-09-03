using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Module.WeChat
{
    public class WeChatException : Exception
    {
        public override string Message
        {
            get
            {
                // 加上前缀
                return "微信Api异常: " + base.Message;
            }
        }

        public WeChatException()
        {
        }

        public WeChatException(string message)
            : base(message)
        {
        }

        public WeChatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
