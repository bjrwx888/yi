using Microsoft.Extensions.Localization;
using Yi.Framework.Common.Models.Enum;
using Yi.Framework.Language;

namespace Yi.Framework.Common.Models
{
    public class Result
    {
        public static IStringLocalizer<LocalLanguage> _local;
        public ResultCode code { get; set; }

        public bool status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public static Result Expire(ResultCode code, string msg="")
        {
            return new Result() {  code = code, status=false,  message = Get(msg, "token_expiration") };
        }
        public static Result Error(string msg = "")
        {
            return new Result() { code = ResultCode.NotSuccess,status=false,  message =Get(msg, "fail") };
        }
        public static Result Success(string msg = "")
        {
            return new Result() {  code = ResultCode.Success,status=true, message =Get( msg, "succeed" )};
        }
        public static Result SuccessError(string msg = "")
        {
            return new Result() { code = ResultCode.Success, status = false, message = Get(msg, "fail") };
        }


        public static Result UnAuthorize(string msg = "")
        {
            return new Result() {  code = ResultCode.NoPermission,status=false, message = Get(msg, "unAuthorize") };
        }
        public Result SetStatus(bool _status)
        {
            this.status = _status;
            return this;
        }
        public Result SetData(object obj)
        {
            this.data = obj;
            return this;
        }
        public Result SetCode(ResultCode Code)
        {
            this.code = Code;
            return this;
        }
        public Result StatusFalse()
        {
            this.status = false;
            return this;
        }
        public Result StatusTrue()
        {
            this.status = true;
            return this;
        }

        public static string Get(string msg,string msg2)
        {
            if (msg=="")
            {
                msg = _local[msg2];
            }
            return msg;
        }
    }
    public class Result<T>
    {
        public ResultCode code { get; set; }
        public string message { get; set; }
        public T data { get; set; }
        public static Result<T> Error(string msg = "fail")
        {
            return new Result<T>() { code = ResultCode.NotSuccess, message = msg };
        }
        public static Result<T> Success(string msg = "succeed")
        {
            return new Result<T>() { code = ResultCode.Success, message = msg };
        }
        public static Result<T> UnAuthorize(string msg = "unAuthorize")
        {
            return new Result<T>() { code = ResultCode.NoPermission, message = msg };
        }

        public Result<T> SetData(T TValue)
        {
            this.data = TValue;
            return this;
        }

        public Result<T> SetCode(ResultCode Code)
        {
            this.code = Code;
            return this;
        }
    }
}