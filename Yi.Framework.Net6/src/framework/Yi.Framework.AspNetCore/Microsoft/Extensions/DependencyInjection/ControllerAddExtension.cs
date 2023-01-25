using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.AspNetCore.Microsoft.Extensions.DependencyInjection
{
    public static class ControllerAddExtension
    {
        public static IMvcBuilder AddNewtonsoftJsonServer(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddNewtonsoftJson(opt =>
               {
                   //忽略循环引用
                   opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                   //不改变字段大小
                   //opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
               });
        }
    }
}
