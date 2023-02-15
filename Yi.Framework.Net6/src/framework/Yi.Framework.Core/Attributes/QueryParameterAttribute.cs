using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Enums;

namespace Yi.Framework.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class QueryParameterAttribute:Attribute
    {
        public QueryParameterAttribute()
        {
        }
        public QueryParameterAttribute(QueryOperatorEnum queryOperatorEnum)
        {
            QueryOperator = queryOperatorEnum;
        }

        public QueryOperatorEnum QueryOperator { get; set; }

        /// <summary>
        /// 验证值为0时是否作为查询条件 
        /// true-作为查询条件 false-不作为查询条件
        /// </summary>
        public bool VerifyIsZero { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public string ColumnName { get; set; }

        public ColumnTypeEnum ColumnType { get; set; }
    }

    public enum ColumnTypeEnum
    { 
        datetime,
        @bool
    }
}
