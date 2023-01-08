using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Base;
using Yi.Framework.Model.ERP.Entitys;

namespace Yi.Framework.DtoModel.ERP.StockDetails
{
    public class StockDetailsCreateUpdateInput : EntityDto<long>
    {
        public long WarehouseId { get; set; }
        public long MaterialId { get; set; }
        public long Number { get; set; }
        public string? Quality { get; set; }
        public StockDetailsTypeEnum StockDetailsType { get; set; }
    }
}
