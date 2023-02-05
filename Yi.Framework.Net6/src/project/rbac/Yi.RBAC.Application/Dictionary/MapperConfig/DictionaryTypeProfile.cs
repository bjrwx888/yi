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
    public class DictionaryTypeProfile: Profile
    {
        public DictionaryTypeProfile()
        {
            CreateMap<DictionaryTypeGetListInputVo, DictionaryTypeEntity>();
            CreateMap<DictionaryTypeCreateInputVo, DictionaryTypeEntity>();
            CreateMap<DictionaryTypeUpdateInputVo, DictionaryTypeEntity>();
            CreateMap<DictionaryTypeEntity, DictionaryTypeGetListOutputDto>();
            CreateMap<DictionaryTypeEntity, DictionaryTypeGetOutputDto>();
        }
    }
}
