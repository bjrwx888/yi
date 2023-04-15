using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;
using Yi.Framework.Infrastructure.Enums;

namespace Yi.Framework.Infrastructure.Exceptions
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
