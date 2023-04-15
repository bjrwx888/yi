using Yi.Framework.Infrastructure.Ddd.Dtos;

namespace Yi.Furion.Core.Rbac.Dtos.Menu
{
    public class MenuGetListInputVo : PagedAndSortedResultRequestDto
    {

        public bool? State { get; set; }
        public string MenuName { get; set; }

    }
}
