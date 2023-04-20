using SqlSugar;
using Yi.Framework.Infrastructure.Data.Entities;
using Yi.Framework.Infrastructure.Ddd.Entities;

namespace Yi.Furion.Core.Bbs.Entities
{
    [SugarTable("Plate")]
    public class PlateEntity : IEntity<long>, ISoftDelete
    {

        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Logo { get; set; }
        public string? Introduction { get; set; }
        public bool IsDeleted { get; set; }
    }
}
