using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Base;

namespace Yi.Framework.DtoModel.ERP.Material
{
    public class MaterialCreateUpdateInput : EntityDto<long>
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? UnitName { get; set; }
        public string? Remarks { get; set; }
    }
}
