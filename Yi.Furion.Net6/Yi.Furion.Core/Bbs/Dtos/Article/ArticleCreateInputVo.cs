using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Furion.Core.Bbs.Dtos.Article
{
    /// <summary>
    /// Article输入创建对象
    /// </summary>
    public class ArticleCreateInputVo
    {
        public string Content { get; set; }
        public string Name { get; set; }
        public long DiscussId { get; set; }
        public long ParentId { get; set; }
    }
}
