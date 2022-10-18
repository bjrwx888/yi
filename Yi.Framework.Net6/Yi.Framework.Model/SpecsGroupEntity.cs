using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using SqlSugar;
namespace Yi.Framework.Model.Models
{
    /// <summary>
    /// 商品规格组表
    ///</summary>
    public partial class SpecsGroupEntity:IBaseModelEntity
    {
        [Navigate(NavigateType.OneToMany, nameof(SpecsEntity.SpecsGroupId))]
        public List<SpecsEntity> Specses { get; set; }
    }
}
