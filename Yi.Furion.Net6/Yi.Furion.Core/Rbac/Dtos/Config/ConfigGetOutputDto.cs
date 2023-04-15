using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;

namespace Yi.Furion.Core.Rbac.Dtos.Config
{
    public class ConfigGetOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
        public string ConfigName { get; set; } = string.Empty;
        public string ConfigKey { get; set; } = string.Empty;
        public string ConfigValue { get; set; } = string.Empty;
        public string ConfigType { get; set; }
        public int OrderNum { get; set; }
        public string Remark { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
