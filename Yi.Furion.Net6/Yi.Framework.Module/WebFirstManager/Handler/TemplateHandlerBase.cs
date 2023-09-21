using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Module.WebFirstManager.Entities;

namespace Yi.Framework.Module.WebFirstManager.Handler
{
    public class TemplateHandlerBase
    {
        protected TableEntity Table { get; set; }
        protected List<FieldEntity> Fields { get; set; }

        public void SetTable(TableEntity table)
        {
            Table = table;
        }

        public void SetFields(List<FieldEntity> fields)
        {
            Fields = fields;
        }
    }
}
