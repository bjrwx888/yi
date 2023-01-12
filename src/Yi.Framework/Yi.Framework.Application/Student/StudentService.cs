using Panda.DynamicWebApi;
using Panda.DynamicWebApi.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Application.Contracts.Student;
using Yi.Framework.Domain.Student;
using Yi.Framework.Domain.Student.IRepository;

namespace Yi.Framework.Application.Student
{
    /// <summary>
    /// 服务实现
    /// </summary>
    [DynamicWebApi]
    public class StudentService : IStudentService, IDynamicWebApi
    {
        private readonly IStudentRepository _studentRepository;
        private readonly StudentManager _studentManager;
        public StudentService(IStudentRepository studentRepository, StudentManager studentManager )
        {
            _studentRepository = studentRepository;
            _studentManager = studentManager;
        }
        public string GetShijie()
        {
            return "你好世界";
        }
    }
}
