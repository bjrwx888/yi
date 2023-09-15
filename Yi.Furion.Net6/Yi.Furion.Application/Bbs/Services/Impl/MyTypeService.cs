using Yi.Framework.Infrastructure.CurrentUsers;
using Yi.Framework.Infrastructure.Data.Filters;
using Yi.Framework.Infrastructure.Ddd.Dtos;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Framework.Infrastructure.Helper;
using Yi.Furion.Core.Bbs.Dtos.MyType;
using Yi.Furion.Core.Bbs.Entities;

namespace Yi.Furion.Application.Bbs.Services.Impl
{
    /// <summary>
    /// Label服务实现
    /// </summary>
    [ApiDescriptionSettings("BBS")]
    public class MyTypeService : CrudAppService<MyTypeEntity, MyTypeOutputDto, MyTypeGetListOutputDto, long, MyTypeGetListInputVo, MyTypeCreateInputVo, MyTypeUpdateInputVo>,
       ILabelService, IDynamicApiController, ITransient
    {
        public MyTypeService(ICurrentUser currentUser, IDataFilter dataFilter)
        {
            _currentUser = currentUser;
            _dataFilter = dataFilter;
        }

        private ICurrentUser _currentUser { get; set; }

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
