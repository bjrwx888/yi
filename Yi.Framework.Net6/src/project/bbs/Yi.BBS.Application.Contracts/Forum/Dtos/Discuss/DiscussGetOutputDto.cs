using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.BBS.Domain.Shared.Forum.EnumClasses;
using Yi.Framework.Ddd.Dtos;
using Yi.RBAC.Application.Contracts.Identity.Dtos;

namespace Yi.BBS.Application.Contracts.Forum.Dtos
{
    public class DiscussGetOutputDto : IEntityDto<long>
    {
        
        public long Id { get; set; }
        public string Title { get; set; }
        public string Types { get; set; }
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
