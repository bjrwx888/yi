using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Base;

namespace Yi.Framework.DtoModel.ERP.PurchaseDetails
{
    public class PurchaseDetailsCreateUpdateInput : EntityDto<long>
    {
        public string? MaterialUnit { get; set; }

        public string? MaterialName { get; set; }
        public float UnitPrice { get; set; }
        public long TotalNumber { get; set; }
        public long? CompleteNumber { get; set; }
        public string? Remarks { get; set; }
    }
}
