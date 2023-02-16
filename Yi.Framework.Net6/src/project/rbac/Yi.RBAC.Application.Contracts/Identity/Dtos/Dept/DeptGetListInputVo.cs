using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Enums;
using Yi.Framework.Ddd.Dtos;

namespace Yi.RBAC.Application.Contracts.Identity.Dtos
{
    [QueryParameter]
    public class DeptGetListInputVo : PagedAllResultRequestDto
    {
        [QueryParameter(QueryOperatorEnum.Equal)]
        public long Id { get; set; }
        [QueryParameter(QueryOperatorEnum.Equal, ColumnType = ColumnTypeEnum.@bool)]
        public bool? State { get; set; }
        [QueryParameter(QueryOperatorEnum.Like)]
        public string? DeptName { get; set; }
        [QueryParameter(QueryOperatorEnum.Equal)]
        public string? DeptCode { get; set; }
        [QueryParameter(QueryOperatorEnum.Equal)]
        public string? Leader { get; set; }

        [QueryParameter(QueryOperatorEnum.GreaterThanOrEqual, ColumnName = "CreationTime",ColumnType =ColumnTypeEnum.datetime)]
        public DateTime? StartTime { get; set; }
        [QueryParameter(QueryOperatorEnum.LessThanOrEqual, ColumnName = "CreationTime", ColumnType = ColumnTypeEnum.datetime)]
        public DateTime? EndTime { get; set; }
    }
}
