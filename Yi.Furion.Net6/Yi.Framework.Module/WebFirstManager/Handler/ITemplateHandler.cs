using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Module.WebFirstManager.Entities;

namespace Yi.Framework.Module.WebFirstManager.Handler
{
    public interface ITemplateHandler
    {
        void SetTable(TableAggregateRoot table);
        HandledTemplate Invoker(string str, string path);
    }
}
