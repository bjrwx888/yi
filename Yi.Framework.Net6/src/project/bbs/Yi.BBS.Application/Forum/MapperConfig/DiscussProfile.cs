using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.BBS.Application.Contracts.Forum.Dtos;
using Yi.BBS.Application.Contracts.Forum.Dtos.Discuss;
using Yi.BBS.Domain.Forum.Entities;

namespace Yi.BBS.Application.Forum.MapperConfig
{
    public class DiscussProfile: Profile
    {
        public DiscussProfile()
        {
            CreateMap<DiscussGetListInputVo, DiscussEntity>();
            CreateMap<DiscussCreateInputVo, DiscussEntity>();
            CreateMap<DiscussUpdateInputVo, DiscussEntity>();
            CreateMap<DiscussEntity, DiscussGetListOutputDto>();
            CreateMap<DiscussEntity, DiscussGetOutputDto>();
        }
    }
}
