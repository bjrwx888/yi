using Yi.Framework.Infrastructure.Ddd.Dtos;

namespace Yi.Framework.Module.DictionaryManager.Dtos.DictionaryType
{
    public class DictionaryTypeGetListInputVo : PagedAllResultRequestDto
    {
        public string? DictName { get; set; }
        public string? DictType { get; set; }
        public string? Remark { get; set; }

        public bool? State { get; set; }
    }
}
