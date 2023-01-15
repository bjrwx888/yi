using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Application.Contracts.Student.Dtos;
using Yi.Framework.Ddd.Services.Abstract;

namespace Yi.Framework.Application.Contracts.Student
{
    /// <summary>
    /// 服务抽象
    /// </summary>
    public interface IStudentService : ICrudAppService<StudentGetOutputDto, StudentGetListOutputDto, long, StudentGetListInputVo, StudentCreateInputVo, StudentUpdateInputVo>
    {

    }
}
