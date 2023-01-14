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

namespace Yi.Framework.Application.Student
{
    /// <summary>
    /// 服务实现
    /// </summary>
    public class StudentService : IStudentService, IAutoApiService
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
        /// <param name="aaa"></param>
        /// <returns></returns>
        public string PostShijie(string aaa)
        {
            return aaa;
        }
    }
}
