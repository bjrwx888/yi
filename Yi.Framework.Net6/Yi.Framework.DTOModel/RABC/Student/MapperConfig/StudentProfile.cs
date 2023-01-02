using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.RABC.Entitys;

namespace Yi.Framework.DtoModel.RABC.Student.MapperConfig
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentCreateInput, StudentEntity>();
            CreateMap<StudentUpdateInput, StudentEntity>();
            CreateMap<StudentCreateUpdateInput, StudentEntity>();
            CreateMap<StudentEntity, StudentGetOutput>();
            CreateMap<StudentEntity, StudentListOutput>();
        }
    }
}
