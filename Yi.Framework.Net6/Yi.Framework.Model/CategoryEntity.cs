using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using SqlSugar;
namespace Yi.Framework.Model.Models
{
    /// <summary>
    /// 商品分类表
    ///</summary>
    public partial class CategoryEntity:IBaseModelEntity
    {
        [SugarColumn(IsIgnore = true)]
       public List<CategoryEntity> Children { get; set; }


        [Navigate(NavigateType.OneToMany,nameof(SpecsGroupEntity.CategoryId))]
        public List<SpecsGroupEntity> SpecsGroups { get; set; }
    }
}