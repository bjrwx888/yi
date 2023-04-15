using Yi.Framework.Infrastructure.Ddd.Dtos;
using Yi.Furion.Core.Bbs.Enums;

namespace Yi.Furion.Core.Bbs.Dtos.Discuss
{
    public class DiscussGetListInputVo : PagedAndSortedResultRequestDto
    {
        public string Title { get; set; }

        public long? PlateId { get; set; }

        //默认查询非置顶
        public bool IsTop { get; set; } = false;


        //查询方式
        public QueryDiscussTypeEnum Type { get; set; } = QueryDiscussTypeEnum.New;
    }
}
