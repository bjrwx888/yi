using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Template.Application.Contracts.School.Dtos;
using Yi.Template.Domain.School.Entities;

namespace Yi.Template.Application.School.MapperConfig
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
