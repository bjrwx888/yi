using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Furion.FriendlyException;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Yi.Framework.Infrastructure.Enums;

namespace Yi.Framework.Infrastructure.Exceptions
{
    public class BusinessException : AppFriendlyException,
        IHasErrorCode,
    IHasErrorDetails,
    IHasLogLevel
    {
        public int Code { get; set; }

        public string? Details { get; set; }

        public LogLevel LogLevel { get; set; }

        public BusinessException(
    int code = (int)ResultCodeEnum.Denied,
    string? message = null,
    string? details = null,
    Exception? innerException = null,
    LogLevel logLevel = LogLevel.Warning)
    : base(message, innerException)
        {
            Code = code;
            Details = details;
            LogLevel = logLevel;
            base.ErrorCode= code;
            base.StatusCode = StatusCodes.Status403Forbidden;
            base.ErrorMessage = $"{message}{(details is not null? ":"+details:"")}";
            base.ValidationException = true;
        }

        /// <summary>
        /// 序列化参数的构造函数
        /// </summary>
        public BusinessException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

    }
}
