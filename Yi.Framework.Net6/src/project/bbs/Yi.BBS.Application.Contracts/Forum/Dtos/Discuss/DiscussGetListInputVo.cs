using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.BBS.Domain.Shared.Forum.ConstClasses;
using Yi.BBS.Domain.Shared.Forum.EnumClasses;
using Yi.Framework.Ddd.Dtos;

namespace Yi.BBS.Application.Contracts.Forum.Dtos.Discuss
{
    public class DiscussGetListInputVo : PagedAndSortedResultRequestDto
    {
        public string? Title { get; set; }

        public long? PlateId { get; set; }

        //默认查询非置顶
        public bool IsTop { get; set; } = false;


        //查询方式
        public QueryDiscussTypeEnum Type { get; set; } = QueryDiscussTypeEnum.New;
    }
}
