using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Yi.Framework.Ddd.Application
{
    public abstract class YiCrudAppService<TEntity, TEntityDto, TKey> : YiCrudAppService<TEntity, TEntityDto, TKey, PagedAndSortedResultRequestDto>
         where TEntity : class, IEntity<TKey>
         where TEntityDto : IEntityDto<TKey>
    {
        protected YiCrudAppService(IRepository<TEntity, TKey> repository) : base(repository)
        {
        }
    }

    public abstract class YiCrudAppService<TEntity, TEntityDto, TKey, TGetListInput>
        : YiCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TEntityDto>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {
        protected YiCrudAppService(IRepository<TEntity, TKey> repository) : base(repository)
        {
        }
    }


    public abstract class YiCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput>
        : YiCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {
        protected YiCrudAppService(IRepository<TEntity, TKey> repository) : base(repository)
        {
        }
    }

    public abstract class YiCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        : YiCrudAppService<TEntity, TEntityDto, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {
        protected YiCrudAppService(IRepository<TEntity, TKey> repository) : base(repository)
        {
        }
    }


    public abstract class YiCrudAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        : CrudAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    where TEntity : class, IEntity<TKey>
    where TGetOutputDto : IEntityDto<TKey>
    where TGetListOutputDto : IEntityDto<TKey>
    {
        protected YiCrudAppService(IRepository<TEntity, TKey> repository) : base(repository)
        {
        }

        /// <summary>
        /// 偷梁换柱
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [RemoteService(isEnabled: true)]
        public async Task DeleteAsync(IEnumerable<TKey> id)
        {
            await Repository.DeleteManyAsync(id);
        }
        [RemoteService(isEnabled:false)]
        public override Task DeleteAsync(TKey id)
        {
            return base.DeleteAsync(id);
        }
    }
}
