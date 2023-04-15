using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Entities;

namespace Yi.Furion.Core.Bbs.Entities
{
    [SugarTable("Setting")]
    public class SettingEntity : IEntity<long>
    {

        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        public int CommentPage { get; set; }
        public int DiscussPage { get; set; }
        public int CommentExperience { get; set; }
        public int DiscussExperience { get; set; }
        public string Title { get; set; }
    }
}
