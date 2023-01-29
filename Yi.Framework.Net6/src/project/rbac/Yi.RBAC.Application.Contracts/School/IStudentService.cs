using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.RBAC.Application.Contracts.School.Dtos;
using Yi.Framework.Ddd.Services.Abstract;

namespace Yi.RBAC.Application.Contracts.School
{
    /// <summary>
    /// Student
    /// </summary>
    public interface IStudentService : ICrudAppService<StudentGetOutputDto, StudentGetListOutputDto, long, StudentGetListInputVo, StudentCreateInputVo, StudentUpdateInputVo>
    {

    }
}
