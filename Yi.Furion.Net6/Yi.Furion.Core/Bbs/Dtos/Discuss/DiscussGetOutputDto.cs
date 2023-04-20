using System;
using System.Collections.Generic;
using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;
using Yi.Furion.Core.Bbs.Enums;
using Yi.Furion.Core.Rbac.Dtos.User;

namespace Yi.Furion.Core.Bbs.Dtos.Discuss
{
    public class DiscussGetOutputDto : IEntityDto<long>
    {

        public long Id { get; set; }
        public string Title { get; set; }
        public string? Types { get; set; }
        public string? Introduction { get; set; }
        public int AgreeNum { get; set; }
        public int SeeNum { get; set; }
        public string Content { get; set; }
        public string? Color { get; set; }

        public long PlateId { get; set; }
        //是否置顶，默认false
        public bool IsTop { get; set; }

        /// <summary>
        /// 封面
        /// </summary>
        public string? Cover { get; set; }
        //是否私有，默认false
        public bool IsPrivate { get; set; }

        //私有需要判断code权限
        public string? PrivateCode { get; set; }
        public DateTime CreationTime { get; set; }
        public DiscussPermissionTypeEnum PermissionType { get; set; }

        public List<long> PermissionUserIds { get; set; }
        public UserGetListOutputDto User { get; set; }
    }
}
