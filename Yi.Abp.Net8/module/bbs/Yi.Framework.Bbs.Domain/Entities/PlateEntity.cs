using SqlSugar;
using Volo.Abp.Domain.Entities;
using Volo.Abp;

namespace Yi.Framework.Bbs.Domain.Entities
{
    [SugarTable("Plate")]
    public class PlateEntity : Entity<Guid>, ISoftDelete
    {

        [SugarColumn(ColumnName = "Id", IsPrimaryKey = true)]
        public override Guid Id { get; protected set; }
        public string Name { get; set; }
        public string? Logo { get; set; }
        public string? Introduction { get; set; }
        public bool IsDeleted { get; set; }
    }
}
