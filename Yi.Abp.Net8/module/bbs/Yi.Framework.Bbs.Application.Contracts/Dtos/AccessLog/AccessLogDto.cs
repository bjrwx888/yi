namespace Yi.Framework.Bbs.Application.Contracts.Dtos.AccessLog
{
    public class AccessLogDto
    {
        public Guid Id { get; set; }
        public long Number { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
