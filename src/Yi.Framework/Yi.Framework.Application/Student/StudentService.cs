using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Yi.Framework.Application.Contracts.Student;
using Yi.Framework.Domain.Student;
using Yi.Framework.Domain.Student.IRepository;


namespace Yi.Framework.Application.Student
{
    /// <summary>
    /// 服务实现
    /// </summary>
    public class StudentService : ApplicationService, IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly StudentManager _studentManager;
        public StudentService(IStudentRepository studentRepository, StudentManager studentManager )
        {
            _studentRepository = studentRepository;
            _studentManager = studentManager;
        }

        /// <summary>
        /// 你好世界
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public string PostShijie(string formFile)
        {
            var ss = formFile;
            return "你好世界";
        }
    }
}
