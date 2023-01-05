using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Common.Enum;
using Yi.Framework.Model.Base;

namespace Yi.Framework.DtoModel.ERP.Warehouse
{
    public class WarehouseCreateUpdateInput : EntityDto<long>
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public StateEnum? State { get; set; }
    }
}
