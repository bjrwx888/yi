using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Base;

namespace Yi.Framework.DtoModel.ERP.Stock
{
    public class StockGetListOutput: EntityDto<long>
    {
        public string MaterialName { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;
        public string WarehouseName { get; set; } = string.Empty;
        public long Number { get; set; }
        public string? Quality { get; set; }
    }
}
