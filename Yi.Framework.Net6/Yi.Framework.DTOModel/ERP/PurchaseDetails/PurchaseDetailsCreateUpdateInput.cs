using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Base;
using Yi.Framework.Model.ERP.Entitys;

namespace Yi.Framework.DtoModel.ERP.PurchaseDetails
{
    public class PurchaseDetailsCreateUpdateInput : EntityDto<long>
    {
        public string MaterialName { get; set; }=string.Empty;
        public string MaterialUnit { get; set; }=string.Empty ;
        public float UnitPrice { get; set; } 
        public long TotalNumber { get; set; } 
        public long CompleteNumber { get; set; }
        public string? Remarks { get; set; }

        public long PurchaseId { get; set; }

    }

    public static class PurchaseDetailsCreateUpdateInputExtensions
    {
        public static List<PurchaseDetailsCreateUpdateInput> SetPurchaseId(this List<PurchaseDetailsCreateUpdateInput> purchaseDetailsEntities, long purchaseId)
        {
            purchaseDetailsEntities.ForEach(u => u.PurchaseId = purchaseId);
            return purchaseDetailsEntities;
        }
    }
}
