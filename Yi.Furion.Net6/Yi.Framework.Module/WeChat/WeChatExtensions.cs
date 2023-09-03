using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Furion;
using Microsoft.Extensions.DependencyInjection;
using Yi.Framework.Module.WeChat.Abstract;

namespace Yi.Framework.Module.WeChat.Extensions
{
    public static class WeChatExtensions
    {
        public static string ToQueryString(this object model)
        {
            var properties = model.GetType().GetProperties();
            var query = HttpUtility.ParseQueryString(string.Empty);

            foreach (var property in properties)
            {
                var value = property.GetValue(model);
                if (value != null)
                {
                    query[property.Name] = value.ToString();
                }
            }

            return "?" + query.ToString();
        }

        /// <summary>
        /// 效验请求是否成功
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static void ValidateSuccess(this IErrorObjct response)
        {

            if (response.errcode != 0)
            {
                throw new WeChatException(response.errmsg);
            }
        }


    }
}
