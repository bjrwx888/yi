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
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<UserGetListInputVo, UserEntity>();
            CreateMap<UserCreateInputVo, UserEntity>();
            CreateMap<UserUpdateInputVo, UserEntity>();
            CreateMap<UserEntity, UserGetListOutputDto>();
            CreateMap<UserEntity, UserGetOutputDto>();
        }
    }
}
