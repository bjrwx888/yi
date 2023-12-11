using Volo.Abp.Application.Dtos;

namespace Yi.Framework.Bbs.Application.Contracts.Dtos.Plate
{
    public class PlateGetListInputVo : PagedAndSortedResultRequestDto
    {
        public string? Name { get; set; }
        public string? Logo { get; set; }
        public string? Introduction { get; set; }
    }
}
