using Yi.Framework.Infrastructure.Ddd.Dtos;

namespace Yi.Furion.Rbac.Application.System.Dtos.Menu
{
    public class MenuGetListInputVo : PagedAndSortedResultRequestDto
    {

        public bool? State { get; set; }
        public string MenuName { get; set; }

    }
}
