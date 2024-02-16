using Yi.Framework.CodeGun.Application.Contracts.Dtos.Field;
using Yi.Framework.Ddd.Application.Contracts;

namespace Yi.Framework.CodeGun.Application.Contracts.IServices
{
    public interface IFieldService : IYiCrudAppService<FieldDto, Guid, FieldGetListInput>
    {
    }
}
