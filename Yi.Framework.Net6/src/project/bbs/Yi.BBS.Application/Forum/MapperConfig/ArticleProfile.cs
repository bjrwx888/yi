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
    public class ArticleProfile: Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleGetListInputVo, ArticleEntity>();
            CreateMap<ArticleCreateInputVo, ArticleEntity>();
            CreateMap<ArticleUpdateInputVo, ArticleEntity>();
            CreateMap<ArticleEntity, ArticleAllOutputDto>();
            CreateMap<ArticleEntity, ArticleGetListOutputDto>();
            CreateMap<ArticleEntity, ArticleGetOutputDto>();
        }
    }
}
