using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.BBS.Application.Contracts.Exhibition.Dtos;
using Yi.BBS.Application.Contracts.Exhibition.Dtos.Banner;
using Yi.BBS.Domain.Exhibition.Entities;

namespace Yi.BBS.Application.Exhibition.MapperConfig
{
    public class BannerProfile: Profile
    {
        public BannerProfile()
        {
            CreateMap<BannerGetListInputVo, BannerEntity>();
            CreateMap<BannerCreateInputVo, BannerEntity>();
            CreateMap<BannerUpdateInputVo, BannerEntity>();
            CreateMap<BannerEntity, BannerGetListOutputDto>();
            CreateMap<BannerEntity, BannerGetOutputDto>();
        }
    }
}
