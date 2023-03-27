using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.BBS.Domain.Shared.Forum.EnumClasses;

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
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        public string Content { get; set; }
        public string? Color { get; set; }

        public long PlateId { get; set; }

        /// <summary>
        /// 默认公开
        /// </summary>
        public DiscussPermissionTypeEnum PermissionType { get; set; } = DiscussPermissionTypeEnum.Public;
        /// <summary>
        /// 封面
        /// </summary>
        public string? Cover { get; set; }
    }
}
