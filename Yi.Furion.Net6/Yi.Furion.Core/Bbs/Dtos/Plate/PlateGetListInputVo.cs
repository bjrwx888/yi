using Yi.Framework.Infrastructure.Ddd.Dtos;

namespace Yi.Furion.Core.Bbs.Dtos.Plate
{
    public class PlateGetListInputVo : PagedAndSortedResultRequestDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Introduction { get; set; }
    }
}
