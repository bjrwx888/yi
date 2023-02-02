using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Base;
using Yi.Framework.Model.ERP.Entitys;

namespace Yi.Framework.DtoModel.ERP.StockDetails
{
    public class StockDetailsGetListOutput: EntityDto<long>
    {
        public long WarehouseName { get; set; }
        public long MaterialName { get; set; }
        public long Number { get; set; }
        public string? Quality { get; set; }
        public DateTime StockDetailsTime { get; set; }
        public StockDetailsTypeEnum StockDetailsType { get; set; }
    }
}
