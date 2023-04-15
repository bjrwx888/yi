using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Enums;

namespace Yi.Framework.Core.Exceptions
{
    public class UserFriendlyException : BusinessException
    {
        public UserFriendlyException(
string message,
int code = (int)ResultCodeEnum.Denied,
string? details = null,
Exception? innerException = null,
LogLevel logLevel = LogLevel.Warning)
: base(
   code,
   message,
   details,
   innerException,
   logLevel)
        {
            Details = details;
        }

        /// <summary>
        /// 序列化参数的构造函数
        /// </summary>
        public UserFriendlyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}
