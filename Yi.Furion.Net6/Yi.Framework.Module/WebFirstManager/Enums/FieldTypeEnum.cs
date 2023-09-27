using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Yi.Framework.Module.WebFirstManager.Enums
{
    public enum FieldTypeEnum
    {
        [Display(Name ="string",Description = "String")]
        String,

        [Display(Name = "int", Description = "Int32")]
        Int,

        [Display(Name = "long", Description = "Int64")]
        Long,
    }
}
