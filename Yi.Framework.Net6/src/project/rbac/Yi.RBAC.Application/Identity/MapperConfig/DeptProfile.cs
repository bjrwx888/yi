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
    public class DeptProfile: Profile
    {
        public DeptProfile()
        {
            CreateMap<DeptGetListInputVo, DeptEntity>();
            CreateMap<DeptCreateInputVo, DeptEntity>();
            CreateMap<DeptUpdateInputVo, DeptEntity>();
            CreateMap<DeptEntity, DeptGetListOutputDto>();
            CreateMap<DeptEntity, DeptGetOutputDto>();
        }
    }
}
