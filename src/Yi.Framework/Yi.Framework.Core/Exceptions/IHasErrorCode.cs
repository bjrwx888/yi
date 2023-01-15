using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Enums;

namespace Yi.Framework.Core.Exceptions
{
    internal interface IHasErrorCode
    {
        ResultCodeEnum Code { get; }
    }
}
