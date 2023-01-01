using AutoMapper;
using AutoMapper.Internal.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Yi.Framework.Interface.Base.Crud;

namespace Yi.Framework.Service.Base.Crud
{
    public class ApplicationService : IApplicationService
    {
        public ApplicationService(IMapper mapper)
        {
            ObjectMapper = mapper;
        }
        protected IMapper ObjectMapper { get; set; }
    }
}