using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Yi.Framework.Ddd.Application;
using Yi.Framework.SqlSugarCore.Abstractions;
using Yi.Framework.TenantManagement.Application.Contracts;
using Yi.Framework.TenantManagement.Application.Contracts.Dtos;
using Yi.Framework.TenantManagement.Domain;

namespace Yi.Framework.TenantManagement.Application
{
    /// <summary>
    /// 租户管理
    /// </summary>
    public class TenantService : YiCrudAppService<TenantAggregateRoot, TenantGetOutputDto, TenantGetListOutputDto, Guid, TenantGetListInput, TenantCreateInput, TenantUpdateInput>, ITenantService
    {
        private ISqlSugarRepository<TenantAggregateRoot, Guid> _repository;
        public TenantService(ISqlSugarRepository<TenantAggregateRoot, Guid> repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 租户单查
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Task<TenantGetOutputDto> GetAsync(Guid id)
        {
            return base.GetAsync(id);
        }

        /// <summary>
        /// 租户多查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override Task<PagedResultDto<TenantGetListOutputDto>> GetListAsync(TenantGetListInput input)
        {
            return base.GetListAsync(input);
        }

        /// <summary>
        /// 创建租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override Task<TenantGetOutputDto> CreateAsync(TenantCreateInput input)
        {
            return base.CreateAsync(input);
        }

        /// <summary>
        /// 更新租户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public override Task<TenantGetOutputDto> UpdateAsync(Guid id, TenantUpdateInput input)
        {
            return base.UpdateAsync(id, input);
        }

        /// <summary>
        /// 租户删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Task DeleteAsync(IEnumerable<Guid> id)
        {
            return base.DeleteAsync(id);
        }
    }
}
