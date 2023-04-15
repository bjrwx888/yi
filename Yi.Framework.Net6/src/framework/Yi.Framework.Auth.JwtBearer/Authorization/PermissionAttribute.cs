using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.CurrentUsers;
using Yi.Framework.Core.Exceptions;
using Yi.Framework.Core.Model;

namespace Yi.Framework.Auth.JwtBearer.Authorization
{

    [AttributeUsage(AttributeTargets.Method)]

    public class PermissionAttribute : ActionFilterAttribute
    {
        internal string Code { get; set; }

        public PermissionAttribute(string code)
        {
            this.Code = code;
        }


    }
}
