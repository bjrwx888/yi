using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.BBS.Application.Contracts.Forum.Dtos;
using Yi.BBS.Domain.Forum.Entities;

namespace Yi.BBS.Application.Forum.MapperConfig
{
    public class MyTypeProfile: Profile
    {
        public MyTypeProfile()
        {
            CreateMap<MyTypeGetListInputVo, MyTypeEntity>();
            CreateMap<MyTypeCreateInputVo, MyTypeEntity>();
            CreateMap<MyTypeUpdateInputVo, MyTypeEntity>();
            CreateMap<MyTypeEntity, MyTypeGetListOutputDto>();
            CreateMap<MyTypeEntity, MyTypeOutputDto>();
        }
    }
}
