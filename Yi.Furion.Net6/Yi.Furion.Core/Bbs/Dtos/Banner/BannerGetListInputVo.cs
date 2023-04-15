using Yi.Framework.Infrastructure.Ddd.Dtos;

namespace Yi.Furion.Core.Bbs.Dtos.Banner
{
    public class BannerGetListInputVo : PagedAndSortedResultRequestDto
    {
        public string Name { get; set; }
    }
}
