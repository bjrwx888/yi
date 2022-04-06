using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Model.Query
{
    public class QueryParameter
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public ConditionalType ConditionalType { get; set; } = ConditionalType.Like;
    }
}
