using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Base;

namespace Yi.Framework.DtoModel.ERP.Stock
{
    public class StockGetListInput
    {

        public long Number { get; set; }
        public string? Quality { get; set; }
    }
}
