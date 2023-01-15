using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Application.Contracts.Student.Dtos;
using Yi.Framework.Domain.Student.Entities;

namespace Yi.Framework.Application.Student.MapperConfig
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
