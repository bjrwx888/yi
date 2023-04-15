using System;
using System.Collections.Generic;
using SqlSugar;
using Yi.Framework.Infrastructure.Data.Auditing;
using Yi.Framework.Infrastructure.Data.Entities;
using Yi.Framework.Infrastructure.Ddd.Entities;
using Yi.Furion.Core.Bbs.Enums;

namespace Yi.Furion.Core.Bbs.Entities
{
    [SugarTable("Discuss")]
    public class DiscussEntity : IEntity<long>, ISoftDelete, IAuditedObject
    {
        public DiscussEntity()
        {
        }
        public DiscussEntity(long plateId)
        {
            PlateId = plateId;
        }

        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        public string Title { get; set; }
        public string Types { get; set; }
        public string Introduction { get; set; }
        public int AgreeNum { get; set; }
        public int SeeNum { get; set; }
        /// <summary>
        /// 封面
        /// </summary>
        public string Cover { get; set; }

        [SugarColumn(Length = 999999)]
        public string Content { get; set; }

        public string Color { get; set; }

        public bool IsDeleted { get; set; }

        //是否置顶，默认false
        public bool IsTop { get; set; }


        public DiscussPermissionTypeEnum PermissionType { get; set; }

        public long PlateId { get; set; }
        public DateTime CreationTime { get; set; }

        public long? CreatorId { get; set; }

        public long? LastModifierId { get; set; }

        public DateTime? LastModificationTime { get; set; }


        /// <summary>
        /// 当PermissionType为部分用户时候，以下列表中的用户+创建者 代表拥有权限
        /// </summary>
        [SugarColumn(IsJson = true)]//使用json处理
        public List<long> PermissionUserIds { get; set; }
    }
}
