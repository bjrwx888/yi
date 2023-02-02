using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.ERP.Entitys;

namespace Yi.Framework.DtoModel.ERP.StockDetails.MapperConfig
{
    public class SuppliERProfile:Profile
    {
        public SuppliERProfile()
        {
            CreateMap<StockDetailsCreateUpdateInput, StockDetailsEntity>();
            CreateMap<StockDetailsEntity, StockDetailsGetListOutput>();

        }
    }
}
