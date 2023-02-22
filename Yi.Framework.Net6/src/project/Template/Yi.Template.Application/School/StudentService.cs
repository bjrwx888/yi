using Yi.Template.Application.Contracts.School;
using Cike.AutoWebApi.Setting;
using Yi.Template.Application.Contracts.School.Dtos;
using Yi.Template.Domain.School.Entities;
using Yi.Framework.Ddd.Services;

namespace Yi.Template.Application.School
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
