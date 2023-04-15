using Yi.Framework.Infrastructure.Ddd.Dtos;

namespace Yi.Furion.Application.Rbac.Dtos.Post
{
    public class PostGetListInputVo : PagedAndSortedResultRequestDto
    {
        public bool? State { get; set; }
        //public string? PostCode { get; set; }=string.Empty;
        public string PostName { get; set; } = string.Empty;
    }
}
