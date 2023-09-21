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
        void SetTable(TableEntity table);
        void SetFields(List<FieldEntity> fields);

        string Invoker(string str);
    }
}
