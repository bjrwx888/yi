using Yi.RBAC.Application.Contracts.School;
using NET.AutoWebApi.Setting;
using Yi.RBAC.Application.Contracts.School.Dtos;
using Yi.RBAC.Domain.School.Entities;
using Yi.Framework.Ddd.Services;

namespace Yi.RBAC.Application.School
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
