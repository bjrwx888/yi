using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.BBS.Application.Contracts.Forum.Dtos.Discuss
{
    /// <summary>
    /// Discuss输入创建对象
    /// </summary>
    public class DiscussCreateInputVo
    {
        public string Title { get; set; }
        public string Types { get; set; }
        public string? Introduction { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Content { get; set; }
        public string? Color { get; set; }
    }
}
