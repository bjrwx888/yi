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
    public class AuthException : Exception,
        IHasErrorCode,
    IHasErrorDetails,
    IHasLogLevel
    {
        public ResultCodeEnum Code { get; set; }

        public string? Details { get; set; }

        public LogLevel LogLevel { get; set; }

        public AuthException(
    ResultCodeEnum code = ResultCodeEnum.NoPermission,
    string? message = null,
    string? details = null,
    Exception? innerException = null,
    LogLevel logLevel = LogLevel.Warning)
    : base(message, innerException)
        {
            Code = code;
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
