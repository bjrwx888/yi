using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.RBAC.Application.Contracts.Dictionary.Dtos;
using Yi.RBAC.Domain.Dictionary.Entities;

namespace Yi.RBAC.Application.Dictionary.MapperConfig
{
    public class DictionaryProfile: Profile
    {
        public DictionaryProfile()
        {
            CreateMap<DictionaryGetListInputVo, DictionaryEntity>();
            CreateMap<DictionaryCreateInputVo, DictionaryEntity>();
            CreateMap<DictionaryUpdateInputVo, DictionaryEntity>();
            CreateMap<DictionaryEntity, DictionaryGetListOutputDto>();
            CreateMap<DictionaryEntity, DictionaryGetOutputDto>();
        }
    }
}
