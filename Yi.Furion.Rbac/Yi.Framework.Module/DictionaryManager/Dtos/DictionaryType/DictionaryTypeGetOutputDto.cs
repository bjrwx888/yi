using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;

namespace Yi.Framework.Module.DictionaryManager.Dtos.DictionaryType
{
    public class DictionaryTypeGetOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public long? CreatorId { get; set; }
        public string DictName { get; set; } = string.Empty;
        public string DictType { get; set; } = string.Empty;
        public string? Remark { get; set; }

        public bool State { get; set; }
    }
}
