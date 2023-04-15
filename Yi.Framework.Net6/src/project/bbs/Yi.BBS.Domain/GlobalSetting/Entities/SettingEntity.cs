using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.Entities;
using Yi.Framework.Ddd.Entities;

namespace Yi.BBS.Domain.GlobalSetting.Entities
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
