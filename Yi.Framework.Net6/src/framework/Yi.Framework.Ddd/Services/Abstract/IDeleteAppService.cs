using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Ddd.Services.Abstract
{
    public interface IDeleteAppService<in TKey> : IApplicationService
    {
        Task<bool> DeleteAsync(string id);
    }
}
