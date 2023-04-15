using Yi.Framework.Infrastructure.Ddd.Dtos;

namespace Yi.Framework.Module.OperLogManager
{
    public class OperationLogGetListInputVo : PagedAllResultRequestDto
    {
        public OperEnum? OperType { get; set; }
        public string? OperUser { get; set; }
    }
}
