using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.DtoModel.ERP.Purchase
{
    /// <summary>
    /// 收货
    /// </summary>
    public class ReceiptInput
    {
        /// <summary>
        /// 采购订单id
        /// </summary>
        public long PurchaseId { get; set; }

    }

    public class ReceiptDetails 
    {
        /// <summary>
        /// 订单子表id
        /// </summary>
        public long PurchaseDatilesId { get; set; }

        /// <summary>
        /// 收货数量
        /// </summary>
        public long Number { get; set; }
    }
}
