using Volo.Abp.DependencyInjection;
using Yi.Framework.CodeGun.Domain.Entities;

namespace Yi.Framework.CodeGun.Domain.Handlers
{
    public interface ITemplateHandler: ISingletonDependency
    {
        void SetTable(TableAggregateRoot table);
        HandledTemplate Invoker(string str, string path);
    }
}
