using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Base;

namespace Yi.Framework.DtoModel.ERP.Unit
{
    public class UnitCreateUpdateInput : EntityDto<long>
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
    }
}
