using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Domain.Student.Entities
{
    /// <summary>
    /// 实体
    /// </summary>
    public class StudentEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
