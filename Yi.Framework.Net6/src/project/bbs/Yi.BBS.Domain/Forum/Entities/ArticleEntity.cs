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
    [SugarTable("Article")]
    public class ArticleEntity : IEntity<long>, ISoftDelete
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        public bool IsDeleted { get; set; }

        public string Content { get; set; }
        public string Name { get; set; }


        public long DiscussId { get; set; }

        public long ParentId { get; set; }

        public List<ArticleEntity> Children { get; set; }
    }
}
