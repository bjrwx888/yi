using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.BBS.Domain.Shared.Forum.EnumClasses;

namespace Yi.BBS.Application.Contracts.Forum.Dtos.Discuss
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
