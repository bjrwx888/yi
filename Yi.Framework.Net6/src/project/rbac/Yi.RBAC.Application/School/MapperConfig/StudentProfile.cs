using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.RBAC.Application.Contracts.School.Dtos;
using Yi.RBAC.Domain.School.Entities;

namespace Yi.RBAC.Application.School.MapperConfig
{
    public class StudentProfile: Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentGetListInputVo, StudentEntity>();
            CreateMap<StudentCreateInputVo, StudentEntity>();
            CreateMap<StudentUpdateInputVo, StudentEntity>();
            CreateMap<StudentEntity, StudentGetListOutputDto>();
            CreateMap<StudentEntity, StudentGetOutputDto>();
        }
    }
}
