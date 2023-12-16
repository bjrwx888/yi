using Yi.Framework.Bbs.Domain.Shared.Enums;

namespace Yi.Framework.Bbs.Application.Contracts.Dtos.Discuss
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

        public List<Guid>? PermissionUserIds { get; set; }

        public DiscussPermissionTypeEnum PermissionType { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string? Cover { get; set; }
    }
}