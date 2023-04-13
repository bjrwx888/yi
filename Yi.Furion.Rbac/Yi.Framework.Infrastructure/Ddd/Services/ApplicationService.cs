using Furion;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Yi.Framework.Infrastructure.Ddd.Services
{
    public abstract class ApplicationService
    {
        public IMapper _mapper { get => App.GetRequiredService<IMapper>(); }
    }
}
