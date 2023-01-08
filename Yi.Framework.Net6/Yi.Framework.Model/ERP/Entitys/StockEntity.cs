using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Yi.Framework.Model.Base;

namespace Yi.Framework.Model.ERP.Entitys
{
    /// <summary>
    /// 库存
    /// </summary>
    [SugarTable("Stock")]
    public class StockEntity : IEntity<long>, IMultiTenant
    {
        /// <summary>
        /// 主键
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 租户id
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 仓库id
        /// </summary>
        public long WarehouseId { get; set; }

        /// <summary>
        /// 物料id
        /// </summary>
        public long MaterialId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public long Number { get; set; }

        /// <summary>
        /// 品质
        /// </summary>
        public string? Quality { get; set; }
    }
}
