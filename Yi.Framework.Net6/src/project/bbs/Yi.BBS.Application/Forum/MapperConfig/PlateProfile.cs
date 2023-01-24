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
    public class PlateProfile: Profile
    {
        public PlateProfile()
        {
            CreateMap<PlateGetListInputVo, PlateEntity>();
            CreateMap<PlateCreateInputVo, PlateEntity>();
            CreateMap<PlateUpdateInputVo, PlateEntity>();
            CreateMap<PlateEntity, PlateGetListOutputDto>();
            CreateMap<PlateEntity, PlateGetOutputDto>();
        }
    }
}
