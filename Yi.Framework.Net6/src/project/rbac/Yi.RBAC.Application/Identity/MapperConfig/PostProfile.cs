using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.RBAC.Application.Contracts.Identity.Dtos;
using Yi.RBAC.Domain.Identity.Entities;

namespace Yi.RBAC.Application.Identity.MapperConfig
{
    public class PostProfile: Profile
    {
        public PostProfile()
        {
            CreateMap<PostGetListInputVo, PostEntity>();
            CreateMap<PostCreateInputVo, PostEntity>();
            CreateMap<PostUpdateInputVo, PostEntity>();
            CreateMap<PostEntity, PostGetListOutputDto>();
            CreateMap<PostEntity, PostGetOutputDto>();
        }
    }
}
