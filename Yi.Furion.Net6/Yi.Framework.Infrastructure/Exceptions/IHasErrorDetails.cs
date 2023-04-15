using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Infrastructure.Exceptions
{
    public interface IHasErrorDetails
    {
        string? Details { get; }
    }
}
