using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;
using Yi.Furion.Core.Rbac.Enums;

namespace Yi.Furion.Core.Rbac.Dtos.Role
{
    public class RoleGetOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public long? CreatorId { get; set; }
        public string RoleName { get; set; }
        public string RoleCode { get; set; }
        public string Remark { get; set; }
        public DataScopeEnum DataScope { get; set; } = DataScopeEnum.ALL;
        public bool State { get; set; }

        public int OrderNum { get; set; }
    }
}
