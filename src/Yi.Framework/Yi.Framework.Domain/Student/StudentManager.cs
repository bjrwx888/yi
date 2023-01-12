using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Domain.Student.IRepository;

namespace Yi.Framework.Domain.Student
{
    /// <summary>
    /// 领域服务
    /// </summary>
    public class StudentManager
    {
        private readonly IStudentRepository _studentRepository;
        public StudentManager(IStudentRepository studentRepository)
        {
            _studentRepository=studentRepository;
        }
    }
}
