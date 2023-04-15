using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;

namespace Yi.Furion.Core.Bbs.Dtos.Banner
{
    public class BannerGetOutputDto : IEntityDto<long>
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Color { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
