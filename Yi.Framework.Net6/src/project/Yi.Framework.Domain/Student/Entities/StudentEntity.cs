using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.Entities;
using Yi.Framework.Ddd.Entities;

namespace Yi.Framework.Domain.Student.Entities
{
    /// <summary>
    /// 学生实体
    /// </summary>
    [SugarTable("Student")]
    public class StudentEntity : IEntity<long>, ISoftDelete
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 学生名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 学号
        /// </summary>
        public long Number { get;set ; }

        /// <summary>
        /// 软删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
