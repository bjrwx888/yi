using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Furion.Core.App.Dtos.Trends;

namespace Yi.Furion.Application.App.Services
{
    /// <summary>
    /// Trends服务抽象
    /// </summary>
    public interface ITrendsService : ICrudAppService<TrendsGetOutputDto, TrendsGetListOutputDto, long, TrendsGetListInput, TrendsCreateInput, TrendsUpdateInputVo>
    {

    }
}
