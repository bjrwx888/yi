using Yi.Framework.Core.Attributes;
using Yi.Framework.Core.DependencyInjection;

namespace Yi.Framework.Web
{
    public class Test2Entity: ITransientDependency
    {
        [Autowired]
        public TestEntity testEntity { get; set; }
    }
}
