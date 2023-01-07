using Castle.DynamicProxy;
using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Common.Base;
using Yi.Framework.Common.Helper;

namespace Yi.Framework.Core.Cache.Aop
{
    public abstract class CacheAOPbase : IInterceptor
    {
        /// <summary>
        /// AOP的拦截方法
        /// </summary>
        /// <param name="invocation"></param>
        public abstract void Intercept(IInvocation invocation);

        /// <summary>
        /// 自定义缓存的key
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        protected string CustomCacheKey(IInvocation invocation)
        {
            var typeName = invocation.TargetType.Name;
            var methodName = invocation.Method.Name;
            var methodArguments = invocation.Arguments.Select(GetArgumentValue).Take(3).ToList();//获取参数列表，最多三个

            string key = $"{typeName}:{methodName}:";
            foreach (var param in methodArguments)
            {
                key = $"{key}{param}:";
            }

            return key.TrimEnd(':');
        }

        /// <summary>
        /// object 转 string
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        protected static string GetArgumentValue(object arg)
        {
            if (arg is DateTime)
                return ((DateTime)arg).ToString("yyyyMMddHHmmss");

            if (!arg.IsNotNull())
                return arg.TryStringNull();

            if (arg != null)
            {
                if (arg is Expression)
                {
                    var obj = arg as Expression;
                    var result = Resolve(obj);
                    return MD5Helper.MD5Encrypt16(result);
                }
                else if (arg.GetType().IsClass)
                {
                    return MD5Helper.MD5Encrypt16(JsonConvert.SerializeObject(arg));
                }

                return $"value:{arg.TryStringNull()}";
            }
            return string.Empty;
        }

        private static string Resolve(Expression expression)
        {
            ExpressionContext expContext = new ExpressionContext();
            expContext.Resolve(expression, ResolveExpressType.WhereSingle);
            var value = expContext.Result.GetString();
            var pars = expContext.Parameters;

            pars.ForEach(s =>
            {
                value = value.Replace(s.ParameterName, s.Value.TryStringNull());
            });

            return value;
        }

    }
}
