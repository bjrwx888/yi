using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.DtoModel.ERP.Purchase
{
    /// <summary>
    /// 回款
    /// </summary>
    public class CollectionInput
    {
        /// <summary>
        /// 采购订单id
        /// </summary>
        public long PurchaseId { get; set; }

        /// <summary>
        ///回款钱
        /// </summary>
        public float CollectionMoney { get; set; }
    }

}
