using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.Entities;
using Yi.Framework.Ddd.Entities;

namespace Yi.RBAC.Domain.School.Entities
{
    [SugarTable("Student")]
    public class StudentEntity : IEntity<long>,ISoftDelete
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        public string Name { get; set; }

        public int? Height { get; set; }

        public string? Phone { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
