using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.Entities;
using Yi.Framework.Ddd.Entities;

namespace Yi.BBS.Domain.Forum.Entities
{
    [SugarTable("Plate")]
    public class PlateEntity : IEntity<long>, ISoftDelete
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        public string Title { get; set; }
        public string PlateType { get; set; }
        public string Introduction { get; set; }
        public DateTime? CreateTime { get; set; }
        public int AgreeNum { get; set; }
        public int SeeNum { get; set; }

        public string Content { get; set; }
        public bool IsDeleted { get; set; }
    }
}
