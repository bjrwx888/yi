using Yi.Framework.CodeGun.Domain.Entities;

namespace Yi.Framework.CodeGun.Domain.Handlers
{
    public class TemplateHandlerBase
    {
        protected TableAggregateRoot Table { get; set; }

        public void SetTable(TableAggregateRoot table)
        {
            Table = table;
        }
    }
}
