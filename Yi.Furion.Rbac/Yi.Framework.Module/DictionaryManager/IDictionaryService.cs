using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Framework.Module.DictionaryManager.Dtos.Dictionary;

namespace Yi.Framework.Module.DictionaryManager
{
    /// <summary>
    /// Dictionary服务抽象
    /// </summary>
    public interface IDictionaryService : ICrudAppService<DictionaryGetOutputDto, DictionaryGetListOutputDto, long, DictionaryGetListInputVo, DictionaryCreateInputVo, DictionaryUpdateInputVo>
    {

    }
}
