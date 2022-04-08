using Newtonsoft.Json.Converters;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Yi.Framework.Model.Query
{
    public class QueryParameter
    {
        public string Key { get; set; }
        public string Value { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ConditionalType Type { get; set; } = ConditionalType.Like;
    }
}
