using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Services.Abstract;
using Yi.Framework.DictionaryManager.Dtos.Dictionary;

namespace Yi.Framework.DictionaryManager
{
    /// <summary>
    /// Dictionary服务抽象
    /// </summary>
    public interface IDictionaryService : ICrudAppService<DictionaryGetOutputDto, DictionaryGetListOutputDto, long, DictionaryGetListInputVo, DictionaryCreateInputVo, DictionaryUpdateInputVo>
    {

    }
}
