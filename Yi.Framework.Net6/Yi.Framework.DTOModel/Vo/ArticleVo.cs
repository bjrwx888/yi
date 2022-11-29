using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.DTOModel.Vo
{
    public class ArticleVo
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public long? UserId { get; set; }

        public long? CreateUser { get; set; }

        public DateTime? CreateTime { get; set; }

        public long? ModifyUser { get; set; }

        public DateTime? ModifyTime { get; set; }

        public bool? IsDeleted { get; set; }

        public long? TenantId { get; set; }

        public int? OrderNum { get; set; }

        public string Remark { get; set; }
        public List<string> Images { get; set; }

        public UserVo User { get; set; }
    }
}
