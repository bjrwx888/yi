using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using SqlSugar;
namespace Yi.Framework.Model.Models
{

    public partial class SpuEntity:IBaseModelEntity
    {
        /// <summary>
        /// 规格Spu完整信息 
        ///</summary>
        [SugarColumn(ColumnName = "SpecsAllInfo", IsJson = true)]
        public List<SpecsSpuAllInfoModel> SpecsSpuAllInfo { get; set; }
        /// <summary>
        /// 规格Spu信息 
        ///</summary>
        [SugarColumn(ColumnName = "SpecsInfo", IsJson = true)]
        public List<SpecsSpuInfoModel> SpecsSpuInfo { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(SkuEntity.SpuId))]
        public List<SpuEntity> Spus { get; set; }
    }

    public class SpecsSpuAllInfoModel
    {
        public string? SpecsGroupName { get; set; }
        public  List<string>? SpecsNames{ get;set;}
    }
    public class SpecsSpuInfoModel
    {
        public long? SpecsGroupId{ get; set; }
        public List<long>? SpecsIds { get; set; }
    }
}
