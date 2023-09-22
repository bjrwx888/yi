using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Framework.Module.WebFirstManager.Dtos.Field;

namespace Yi.Framework.Module.WebFirstManager
{
    public interface IFieldService : ICrudAppService<FieldDto, long, FieldGetListInput>
    {
    }
}
