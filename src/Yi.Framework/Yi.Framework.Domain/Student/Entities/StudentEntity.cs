using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Entities;

namespace Yi.Framework.Domain.Student.Entities
{
    /// <summary>
    /// 学生实体
    /// </summary>
    [SugarTable("Student")]
    public class StudentEntity : IEntity<long>
    {
        public long Id { get; set; }

        /// <summary>
        /// 学生名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
