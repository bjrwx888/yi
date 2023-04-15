using Yi.Framework.Infrastructure.Ddd.Dtos;

namespace Yi.Furion.Core.Rbac.Dtos.LoginLog
{
    public class LoginLogGetListInputVo : PagedAllResultRequestDto
    {
        public string LoginUser { get; set; }

        public string LoginIp { get; set; }
    }
}
