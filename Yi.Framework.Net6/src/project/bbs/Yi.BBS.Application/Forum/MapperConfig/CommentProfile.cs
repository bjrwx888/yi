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
    public class CommentProfile: Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentGetListInputVo, CommentEntity>();
            CreateMap<CommentCreateInputVo, CommentEntity>();
            CreateMap<CommentUpdateInputVo, CommentEntity>();
            CreateMap<CommentEntity, CommentGetListOutputDto>();
            CreateMap<CommentEntity, CommentGetOutputDto>();
        }
    }
}
