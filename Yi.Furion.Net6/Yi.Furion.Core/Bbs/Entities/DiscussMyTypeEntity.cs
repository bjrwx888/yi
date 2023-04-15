using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Infrastructure.Ddd.Entities;

namespace Yi.Furion.Core.Bbs.Entities
{
    [SugarTable("DiscussMyType")]
    public class DiscussMyTypeEntity : IEntity<long>
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        public long DiscussId { get; set; }

        public long MyTypeId { get; set; }
    }
}
