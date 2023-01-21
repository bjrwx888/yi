using Yi.Framework.Application.Contracts.School;
using NET.AutoWebApi.Setting;
using Yi.Framework.Application.Contracts.School.Dtos;
using Yi.Framework.Domain.School.Entities;
using Yi.Framework.Ddd.Services;

namespace Yi.Framework.Application.School
{
    /// <summary>
    /// Student服务实现
    /// </summary>
    [AppService]
    public class StudentService : CrudAppService<StudentEntity, StudentGetOutputDto, StudentGetListOutputDto, long, StudentGetListInputVo, StudentCreateInputVo, StudentUpdateInputVo>,
       IStudentService, IAutoApiService
    {
    }
}
