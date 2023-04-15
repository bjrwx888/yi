using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Template.Application.Contracts.School.Dtos
{
    /// <summary>
    /// Student输入创建对象
    /// </summary>
    public class StudentCreateInputVo
    {
        public string Name { get; set; }
        public int? Height { get; set; }
        public string? Phone { get; set; }
    }
}
