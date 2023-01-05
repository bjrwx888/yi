using SqlSugar;
using System;
using Yi.Framework.Model.Base;

namespace Yi.Framework.Model.RABC.Entitys
{
    [SugarTable("Student")]
    public class StudentEntity : IEntity<Guid>, IMultiTenant
    {
        public string? Name { get; set; }
        public string? Remark { get; set; }

        [SugarColumn(ColumnName = "Id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [SugarColumn(ColumnName = "TenantId")]
        public Guid? TenantId { get; set; }

    }
}