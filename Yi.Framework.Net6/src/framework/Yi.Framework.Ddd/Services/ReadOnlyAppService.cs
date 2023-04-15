using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Attributes;
using Yi.Framework.Core.Helper;
using Yi.Framework.Core.Model;
using Yi.Framework.Ddd.Dtos;
using Yi.Framework.Ddd.Entities;
using Yi.Framework.Ddd.Repositories;
using Yi.Framework.Ddd.Services.Abstract;

namespace Yi.Framework.Ddd.Services
{

    public abstract class ReadOnlyAppService<TEntity, TEntityDto, TKey>
    : ReadOnlyAppService<TEntity, TEntityDto, TEntityDto, TKey, PagedAndSortedResultRequestDto>
    where TEntity : class, IEntity<TKey>
    where TEntityDto : IEntityDto<TKey>
    {
    }

    public abstract class ReadOnlyAppService<TEntity, TEntityDto, TKey, TGetListInput>
: ReadOnlyAppService<TEntity, TEntityDto, TEntityDto, TKey, TGetListInput>
where TEntity : class, IEntity<TKey>
where TEntityDto : IEntityDto<TKey>
    {
    }


    public abstract class ReadOnlyAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput> : ApplicationService,
      IReadOnlyAppService<TGetOutputDto, TGetListOutputDto, TKey, TGetListInput>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// 先暂时用服务定位的方式，之后将更改为属性注入
        /// </summary>
        protected IRepository<TEntity> _repository { get => ServiceLocatorModel.Instance.GetRequiredService<IRepository<TEntity>>(); }

        protected ISugarQueryable<TEntity> _DbQueryable => _repository._DbQueryable;

        //Mapper
        protected virtual Task<TGetOutputDto> MapToGetOutputDtoAsync(TEntity entity)
        {
            return Task.FromResult(_mapper.Map<TEntity, TGetOutputDto>(entity));
        }
        protected virtual Task<List<TGetListOutputDto>> MapToGetListOutputDtosAsync(List<TEntity> entities)
        {
            var dtos = _mapper.Map<List<TGetListOutputDto>>(entities);

            return Task.FromResult(dtos);
        }
        protected virtual Task<TGetListOutputDto> MapToGetListOutputDtoAsync(TEntity entity)
        {
            var dto = _mapper.Map<TEntity, TGetListOutputDto>(entity);
            return Task.FromResult(dto);
        }

        /// <summary>
        /// 单查
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual async Task<TGetOutputDto> GetAsync(TKey id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = await _repository.GetByIdAsync(id);

            return await MapToGetOutputDtoAsync(entity);
        }

        /// <summary>
        /// 多查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<PagedResultDto<TGetListOutputDto>> GetListAsync(TGetListInput input)
        {
            var totalCount = -1;

            var entities = new List<TEntity>();
            var entityDtos = new List<TGetListOutputDto>();

            bool isPageList = true;

            //if (totalCount > 0)
            //{

                //这里还可以追加如果是审计日志，继续拼接条件即可
                if (input is IPageTimeResultRequestDto timeInput)
                {
                    if (timeInput.StartTime is not null)
                    {
                        timeInput.EndTime = timeInput.EndTime ?? DateTime.Now;
                    }
                }




                if (input is IPagedAndSortedResultRequestDto sortInput)
                { 
                    entities = await _repository.GetPageListAsync(_ => true, sortInput,sortInput.SortBy, sortInput.SortType);
                }


                else
                {
                    isPageList = false;
                    entities = await _repository.GetListAsync();
                }
                entityDtos = await MapToGetListOutputDtosAsync(entities);
            //}

            //如果是分页查询，还需要统计数量
            if (isPageList)
            {
                totalCount = await _repository.CountAsync(_ => true);
            }
            return new PagedResultDto<TGetListOutputDto>(
                totalCount,
                entityDtos
            );
        }
    }
}
