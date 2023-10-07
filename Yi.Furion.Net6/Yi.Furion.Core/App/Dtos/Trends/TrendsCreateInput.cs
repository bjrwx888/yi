namespace Yi.Furion.Core.App.Dtos.Trends
{
    /// <summary>
    /// Trends输入创建对象
    /// </summary>
    public class TrendsCreateInput
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? Remark { get; set; }
        public List<long>? Images { get; set; }

    }
}
