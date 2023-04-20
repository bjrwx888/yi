using SqlSugar;
using Yi.Framework.Infrastructure.Data.Entities;
using Yi.Framework.Infrastructure.Ddd.Entities;

namespace Yi.Furion.Core.Bbs.Entities
{
    [SugarTable("MyType")]
    public class MyTypeEntity : IEntity<long>, ISoftDelete
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        public bool IsDeleted { get; set; }

        public string Name { get; set; }
        public string? Color { get; set; }
        public string? BackgroundColor { get; set; }

        public long UserId { get; set; }
    }
}
