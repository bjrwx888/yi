﻿using System;
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

        public override async Task<PagedResultDto<TGetListOutputDto>> GetListAsync(TGetListInput input)
        {
            List<TEntity>? entites = null;
            //区分多查还是批量查
            if (input is IPagedResultRequest pagedInput)
            {
                entites = await Repository.GetPagedListAsync(pagedInput.SkipCount, pagedInput.MaxResultCount, string.Empty);
            }
            else
            {
                entites = await Repository.GetListAsync();
            }
            var total = await Repository.CountAsync();
            var output = await MapToGetListOutputDtosAsync(entites);
            return new PagedResultDto<TGetListOutputDto>(total, output);
            //throw new NotImplementedException($"【{typeof(TEntity)}】实体的CrudAppService，查询为具体业务，通用查询几乎无实际场景，请重写实现！");
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
        [RemoteService(isEnabled: false)]
        public override Task DeleteAsync(TKey id)
        {
            return base.DeleteAsync(id);
        }
    }
}