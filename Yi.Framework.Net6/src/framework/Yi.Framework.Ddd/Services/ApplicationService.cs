using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

using Yi.Framework.Core.Model;

namespace Yi.Framework.Ddd.Services
{
    public abstract class ApplicationService
    { 
        public IMapper _mapper { get => ServiceLocatorModel.Instance.GetRequiredService<IMapper>(); }
    }
}
