using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Framework.Module.WebFirstManager.Dtos.Template;

namespace Yi.Framework.Module.WebFirstManager
{
    public interface ITemplateService : ICrudAppService<TemplateDto, long, TemplateGetListInput>
    {
    }
}
