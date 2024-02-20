using SqlSugar;
using Volo.Abp.Application.Dtos;
using Yi.Framework.Ddd.Application;
using Yi.Framework.Rbac.Application.Contracts.Dtos.Notice;
using Yi.Framework.Rbac.Application.Contracts.IServices;
using Yi.Framework.Rbac.Domain.Entities;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.Rbac.Application.Services
{
    /// <summary>
    /// Notice服务实现
    /// </summary>
    public class NoticeService : YiCrudAppService<NoticeEntity, NoticeGetOutputDto, NoticeGetListOutputDto, Guid, NoticeGetListInput, NoticeCreateInput, NoticeUpdateInput>,
       INoticeService
    {
        private ISqlSugarRepository<NoticeEntity, Guid> _repository;
        public NoticeService(ISqlSugarRepository<NoticeEntity, Guid> repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 多查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<PagedResultDto<NoticeGetListOutputDto>> GetListAsync(NoticeGetListInput input)
        {
            RefAsync<int> total = 0;

            var entities = await _repository._DbQueryable.WhereIF(input.Type is not null, x => x.Type==input.Type)
                          .WhereIF(!string.IsNullOrEmpty(input.Title), x => x.Title!.Contains(input.Title!))
                          .WhereIF(input.StartTime is not null && input.EndTime is not null, x => x.CreationTime >= input.StartTime && x.CreationTime <= input.EndTime)
                          .ToPageListAsync(input.SkipCount, input.MaxResultCount, total);
            return new PagedResultDto<NoticeGetListOutputDto>(total, await MapToGetListOutputDtosAsync(entities));
        }
    }
}
