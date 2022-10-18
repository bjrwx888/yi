using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using SqlSugar;
namespace Yi.Framework.Model.Models
{
    /// <summary>
    /// Sku表
    ///</summary>
    public partial class SkuEntity:IBaseModelEntity
    {
        /// <summary>
        /// 规格sku信息 
        ///</summary>
        [SugarColumn(ColumnName = "SpecsSkuInfo", IsJson = true)]
        public List<SpecsSkuInfoModel> SpecsSkuInfo { get; set; }
        /// <summary>
        /// 规格sku完整信息 
        ///</summary>
        [SugarColumn(ColumnName = "SpecsSkuAllInfo", IsJson = true)]
        public List<SpecsSkuAllInfoModel> SpecsSkuAllInfo { get; set; }
    }

    public class SpecsSkuAllInfoModel
    {
        public string? SpecsGroupName { get; set; }
        public string? SpecsName { get; set; }
    }
    public class SpecsSkuInfoModel
    {
        public long? SpecsGroupId { get; set; }
        public long? SpecsId { get; set; }
    }
}
