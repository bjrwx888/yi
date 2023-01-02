using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.DtoModel.ERP.Supplier;
using Yi.Framework.Interface.ERP;
using Yi.Framework.Model.ERP.Entitys;
using Yi.Framework.Repository;
using Yi.Framework.Service.Base.Crud;

namespace Yi.Framework.Service.ERP
{
    public class SupplierService : CrudAppService<SupplierEntity, SupplierGetListOutput, long, SupplierCreateUpdateInput>, ISupplierService
    {
        public SupplierService(IRepository<SupplierEntity> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<List<SupplierGetListOutput>> GetListAsync()
        {
            return await MapToGetListOutputDtosAsync(await Repository.GetListAsync());
        }
    }
}
