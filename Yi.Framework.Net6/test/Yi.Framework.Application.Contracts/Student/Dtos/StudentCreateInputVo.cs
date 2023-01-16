using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Application.Contracts.Student.Dtos
{
    /// <summary>
    /// Student输入创建对象
    /// </summary>
    public class StudentCreateInputVo
    {
        public string Name { get; set; }

        public long Number { get; set; }
    }
}
