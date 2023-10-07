namespace Yi.Furion.Core.App.Dtos.Trends
{
    public class TrendsUpdateInputVo
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Remark { get; set; }
        public List<long>? Images { get; set; }
    }
}
