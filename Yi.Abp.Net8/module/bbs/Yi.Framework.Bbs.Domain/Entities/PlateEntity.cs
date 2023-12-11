using SqlSugar;
using Volo.Abp.Domain.Entities;
using Volo.Abp;
using Volo.Abp.Auditing;

namespace Yi.Framework.Bbs.Domain.Entities
{
    [SugarTable("Plate")]
    public class PlateEntity : Entity<Guid>, ISoftDelete,IAuditedObject
    {

        [SugarColumn(ColumnName = "Id", IsPrimaryKey = true)]
        public override Guid Id { get; protected set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string? Logo { get; set; }
        public string? Introduction { get; set; }
        public bool IsDeleted { get; set; }



        public DateTime CreationTime { get; set; }

        public Guid? CreatorId { get; set; }

        public Guid? LastModifierId { get; set; }

        public DateTime? LastModificationTime { get; set; }
    }
}
