using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yi.Framework.Common.Const;
using Yi.Framework.Common.Enum;

namespace Yi.Framework.WebCore.AttributeExtend
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LogAttribute : Attribute
    {
        private OperationEnum OperationType { get; set; }

        public LogAttribute(OperationEnum operationType)
        {
            this.OperationType = operationType;
        }
    }
}