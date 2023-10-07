using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Yi.Framework.Infrastructure.Data.Auditing;
using Yi.Framework.Infrastructure.Data.Entities;
using Yi.Framework.Infrastructure.Ddd.Entities;
using Yi.Framework.Infrastructure.Helper;

namespace Yi.Furion.Core.App.Entities
{
    /// <summary>
    /// 动态
    /// </summary>
    [SugarTable("Trends")]
    public class TrendsEntity : AuditedObject, IEntity<long>, ISoftDelete
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        public string Title { get; set; }


        [SugarColumn(Length = 99999)]
        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public string? Remark { get; set; }

        [SugarColumn(IsJson = true, Length = 99999)]
        public List<long> Images { get; set; }
    }


}
