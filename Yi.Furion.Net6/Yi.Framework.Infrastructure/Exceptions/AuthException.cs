using System.Runtime.Serialization;
using Furion.FriendlyException;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Yi.Framework.Infrastructure.Enums;

namespace Yi.Framework.Infrastructure.Exceptions
{
    public class AuthException : AppFriendlyException,
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


            base.ErrorCode = code;
            base.StatusCode = StatusCodes.Status401Unauthorized;
            base.ErrorMessage = $"{message}{(details is not null ? ":" + details : "")}";
            base.ValidationException = true;
        }

        /// <summary>
        /// 序列化参数的构造函数
        /// </summary>
        public AuthException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }



    }
}
