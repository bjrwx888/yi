using System.Collections.Generic;
using Yi.Furion.Core.Bbs.Enums;

namespace Yi.Furion.Core.Bbs.Dtos.Discuss
{
    public class DiscussUpdateInputVo
    {
        public string Title { get; set; }
        public string? Types { get; set; }
        public string? Introduction { get; set; }
        public int AgreeNum { get; set; }
        public int SeeNum { get; set; }
        public string Content { get; set; }
        public string? Color { get; set; }

        public List<long> PermissionUserIds { get; set; }

        public DiscussPermissionTypeEnum PermissionType { get; set; }

        /// <summary>
        /// ∑‚√Ê
        /// </summary>
        public string? Cover { get; set; }
    }
}
