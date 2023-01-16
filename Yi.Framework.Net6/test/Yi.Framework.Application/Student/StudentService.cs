using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Application.Contracts.Student;
using Yi.Framework.Domain.Student;
using Yi.Framework.Domain.Student.IRepository;
using Microsoft.AspNetCore.Mvc;
using NET.AutoWebApi.Setting;
using Microsoft.AspNetCore.Http;
using Yi.Framework.Ddd.Services.Abstract;
using Yi.Framework.Application.Contracts.Student.Dtos;
using Yi.Framework.Domain.Student.Entities;
using Yi.Framework.Ddd.Services;

namespace Yi.Framework.Application.Student
{
    /// <summary>
    /// 服务实现
    /// </summary>
    public class StudentService : CrudAppService<StudentEntity, StudentGetOutputDto, StudentGetListOutputDto, long, StudentGetListInputVo, StudentCreateInputVo, StudentUpdateInputVo>,
        IStudentService, IAutoApiService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly StudentManager _studentManager;
        public StudentService(IStudentRepository studentRepository, StudentManager studentManager)
        {
            _studentRepository = studentRepository;
            _studentManager = studentManager;
        }
        /// <summary>
        /// 你好世界
        /// </summary>
        /// <returns></returns>
        public async Task<List<StudentGetListOutputDto>> PostShijie()
        {
            throw new NotImplementedException();
            var entities = await _studentRepository.GetMyListAsync();
            return await MapToGetListOutputDtosAsync(entities);
        }
    }
}
