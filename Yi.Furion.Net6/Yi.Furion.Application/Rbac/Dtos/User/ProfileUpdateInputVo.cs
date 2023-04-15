using Yi.Furion.Core.Rbac.EnumClasses;

namespace Yi.Furion.Application.Rbac.Dtos.User
{
    public class ProfileUpdateInputVo
    {
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Nick { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long? Phone { get; set; }
        public string Introduction { get; set; }
        public string Remark { get; set; }
        public SexEnum? Sex { get; set; }
    }
}
