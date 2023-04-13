using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;

namespace Yi.Framework.Module.OperLogManager
{
    public class OperationLogGetListOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public OperEnum OperType { get; set; }
        public string? RequestMethod { get; set; }
        public string? OperUser { get; set; }
        public string? OperIp { get; set; }
        public string? OperLocation { get; set; }
        public string? Method { get; set; }
        public string? RequestParam { get; set; }
        public string? RequestResult { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
