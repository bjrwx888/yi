using Yi.Framework.CodeGun.Application.Contracts.Dtos.Template;
using Yi.Framework.Ddd.Application.Contracts;

namespace Yi.Framework.CodeGun.Application.Contracts.IServices
{
    public interface ITemplateService : IYiCrudAppService<TemplateDto, Guid, TemplateGetListInput>
    {
    }
}
