using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Yi.Framework.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]

    public class PermissionAttribute : ActionFilterAttribute
    {
        internal string Code { get; set; }

        public PermissionAttribute(string code)
        {
            Code = code;
        }


    }
}
