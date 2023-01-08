using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Base;

namespace Yi.Framework.DtoModel.ERP.Stock
{
    public class StockCreateUpdateInput : EntityDto<long>
    {
        public long WarehouseId { get; set; }
        public long MaterialId { get; set; }
        public long Number { get; set; }
        public string? Quality { get; set; }
    }
}
