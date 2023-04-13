using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;
using Yi.Framework.Infrastructure.Enums;

namespace Yi.Framework.Infrastructure.Exceptions
{
    public class AuthException : Exception,
        IHasErrorCode,
    IHasErrorDetails,
    IHasLogLevel
    {
        public int Code { get; set; }

        public string? Details { get; set; }

        public LogLevel LogLevel { get; set; }

        public AuthException(

    string? message = null,
        ResultCodeEnum code = ResultCodeEnum.NoPermission,
    string? details = null,
    Exception? innerException = null,
    LogLevel logLevel = LogLevel.Warning)
    : base(message, innerException)
        {
            Code = (int)code;
            Details = details;
            LogLevel = logLevel;
        }

        /// <summary>
        /// 序列化参数的构造函数
        /// </summary>
        public AuthException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        public AuthException WithData(string name, object value)
        {
            Data[name] = value;
            return this;
        }

    }
}
