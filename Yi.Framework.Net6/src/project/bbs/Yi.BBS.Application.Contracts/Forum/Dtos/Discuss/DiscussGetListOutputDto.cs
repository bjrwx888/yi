using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;
using Yi.RBAC.Application.Contracts.Identity.Dtos;

namespace Yi.BBS.Application.Contracts.Forum.Dtos.Discuss
{
    public class DiscussGetListOutputDto : IEntityDto<long>
    {
        /// <summary>
        /// 是否已点赞
        /// </summary>
        public bool IsAgree { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Types { get; set; }
        public string? Introduction { get; set; }

        public int AgreeNum { get; set; }
        public int SeeNum { get; set; }

        //批量查询，不给内容，性能考虑
        //public string Content { get; set; }
        public string? Color { get; set; }

        public long PlateId { get; set; }

        //是否置顶，默认false
        public bool IsTop { get; set; }


        //是否私有，默认false
        public bool IsPrivate { get; set; }

        //私有需要判断code权限
        public string? PrivateCode { get; set; }
        public DateTime CreationTime { get; set; }
        public UserGetListOutputDto User { get; set; }
    }
}
