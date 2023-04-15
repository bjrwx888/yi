using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Framework.Module.DictionaryManager.Dtos.DictionaryType;

namespace Yi.Framework.Module.DictionaryManager
{
    /// <summary>
    /// DictionaryType服务抽象
    /// </summary>
    public interface IDictionaryTypeService : ICrudAppService<DictionaryTypeGetOutputDto, DictionaryTypeGetListOutputDto, long, DictionaryTypeGetListInputVo, DictionaryTypeCreateInputVo, DictionaryTypeUpdateInputVo>
    {

    }
}
