using Yi.BBS.Application.Contracts.Forum;
using NET.AutoWebApi.Setting;
using Yi.BBS.Application.Contracts.Forum.Dtos;
using Yi.BBS.Domain.Forum.Entities;
using Yi.Framework.Ddd.Services;
using Yi.Framework.Core.CurrentUsers;
using SqlSugar;
using Yi.Framework.Ddd.Dtos;
using Yi.Framework.Data.Filters;

namespace Yi.BBS.Application.Forum
{
    /// <summary>
    /// Label服务实现
    /// </summary>
    [AppService]
    public class MyTypeService : CrudAppService<MyTypeEntity, MyTypeOutputDto, MyTypeGetListOutputDto, long, MyTypeGetListInputVo, MyTypeCreateInputVo, MyTypeUpdateInputVo>,
       ILabelService, IAutoApiService
    {
        [Autowired]
        private ICurrentUser _currentUser { get; set; }

        [Autowired]
        private IDataFilter _dataFilter { get; set; }

        /// <summary>
        /// 获取当前用户的主题类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PagedResultDto<MyTypeGetListOutputDto>> GetListCurrentAsync(MyTypeGetListInputVo input)
        {

            _dataFilter.AddFilter<MyTypeEntity>(x => x.UserId == _currentUser.Id);
            return base.GetListAsync(input);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<MyTypeOutputDto> CreateAsync(MyTypeCreateInputVo input)
        {
            var entity = await MapToEntityAsync(input);
            entity.Id = SnowflakeHelper.NextId;
            entity.UserId = _currentUser.Id;
            entity.IsDeleted = false;
            var outputEntity = await _repository.InsertReturnEntityAsync(entity);
            return await MapToGetOutputDtoAsync(outputEntity);
        }
    }
}
