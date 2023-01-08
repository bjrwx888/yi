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
    /// 库存明细，像这种记录型的表，需要进行冗余字段保存历史记录
    /// </summary>
    [SugarTable("StockDetails")]
    public class StockDetailsEntity : IEntity<long>, IMultiTenant
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
        /// 库存id
        /// </summary>
        public long StockId { get; set; }
        /// <summary>
        /// 仓库id
        /// </summary>
        public long WarehouseId { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WarehouseName { get; set; } = string.Empty;

        /// <summary>
        /// 物料id
        /// </summary>
        public long MaterialId { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }=string.Empty;


        /// <summary>
        /// 数量
        /// </summary>
        public long Number { get; set; }

        /// <summary>
        /// 品质
        /// </summary>
        public string? Quality { get; set; }


        /// <summary>
        /// 入库或者出库时间
        /// </summary>
        public DateTime StockDetailsTime { get; set; }

        /// <summary>
        /// 明细类别
        /// </summary>
        public StockDetailsTypeEnum StockDetailsType { get; set; }
    }
    public enum StockDetailsTypeEnum
    {
        Input = 0,//入库
        Output = 1//出库
    }
}
