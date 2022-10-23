using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using SqlSugar;
namespace Yi.Framework.Model.Models
{
    public partial class ArticleEntity:IBaseModelEntity
    {
        [Navigate(NavigateType.OneToOne,nameof(UserId))]
        public UserEntity? User { get; set; }
    }
}
