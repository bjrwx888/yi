using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;

namespace Yi.Furion.Core.Bbs.Dtos.MyType
{
    public class MyTypeOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string BackgroundColor { get; set; }
        public long UserId { get; set; }
    }
}
