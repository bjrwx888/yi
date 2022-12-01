using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.DTOModel.Vo;
using Yi.Framework.Model.Models;

namespace Yi.Framework.WebCore.Mapper
{
    public class AutoMapperProfile : Profile
    {
        // 添加你的实体映射关系. 
        public AutoMapperProfile()
        {
            CreateMap<ArticleEntity, ArticleVo > ();
            CreateMap<UserEntity, UserVo>();
            CreateMap<CommentEntity, CommentVo>();
        }
    }

  
}
