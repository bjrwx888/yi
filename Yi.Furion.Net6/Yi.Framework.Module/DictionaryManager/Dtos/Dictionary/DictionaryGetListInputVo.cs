using Yi.Framework.Infrastructure.Ddd.Dtos;

namespace Yi.Framework.Module.DictionaryManager.Dtos.Dictionary
{
    public class DictionaryGetListInputVo : PagedAndSortedResultRequestDto
    {
        public string? DictType { get; set; }
        public string? DictLabel { get; set; }
        public bool? State { get; set; }
    }
}
