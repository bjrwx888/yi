using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Yi.Framework.Data.Entities;
using Yi.Framework.Ddd.Entities;

namespace Yi.BBS.Domain.Forum.Entities
{
    [SugarTable("Discuss")]
    public class DiscussEntity : IEntity<long>, ISoftDelete
    {
        public DiscussEntity()
        { 
        }
        public DiscussEntity(long plateId)
        {
            PlateId = plateId;
        }

        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        public string Title { get; set; }
        public string Types { get; set; }
        public string? Introduction { get; set; }
        public DateTime? CreateTime { get; set; }
        public int AgreeNum { get; set; }
        public int SeeNum { get; set; }

        public string Content { get; set; }

        public string? Color { get; set; }

        public bool IsDeleted { get; set; }


        public long PlateId { get; set; }
    }
}
