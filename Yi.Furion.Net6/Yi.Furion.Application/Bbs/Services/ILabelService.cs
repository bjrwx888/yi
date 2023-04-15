using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Furion.Core.Bbs.Dtos.MyType;

namespace Yi.Furion.Application.Bbs.Services
{
    /// <summary>
    /// Label服务抽象
    /// </summary>
    public interface ILabelService : ICrudAppService<MyTypeOutputDto, MyTypeGetListOutputDto, long, MyTypeGetListInputVo, MyTypeCreateInputVo, MyTypeUpdateInputVo>
    {

    }
}
