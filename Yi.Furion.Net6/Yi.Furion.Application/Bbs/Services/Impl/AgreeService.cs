using Yi.Framework.Infrastructure.CurrentUsers;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Framework.Infrastructure.Exceptions;
using Yi.Furion.Core.Bbs.Dtos.Argee;
using Yi.Furion.Core.Bbs.Entities;

namespace Yi.Furion.Application.Bbs.Services.Impl
{
    /// <summary>
    /// 点赞功能
    /// </summary>
    [ApiDescriptionSettings("BBS")]
    public class AgreeService : ApplicationService, IApplicationService, IDynamicApiController, ITransient
    {
        public AgreeService(IRepository<AgreeEntity> repository, IRepository<DiscussEntity> discssRepository, ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
            _discssRepository = discssRepository;
        }

        private IRepository<AgreeEntity> _repository { get; set; }

        private IRepository<DiscussEntity> _discssRepository { get; set; }
        private ICurrentUser _currentUser { get; set; }


        /// <summary>
        /// 点赞,返回true为点赞+1，返回false为点赞-1
        /// </summary>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<AgreeDto> PostOperateAsync(long discussId)
        {
            var entity = await _repository.GetFirstAsync(x => x.DiscussId == discussId && x.CreatorId == _currentUser.Id);
            //判断是否已经点赞过
            if (entity is null)
            {

                //没点赞过，添加记录即可，,修改总点赞数量
                await _repository.InsertAsync(new AgreeEntity(discussId));
                var discussEntity = await _discssRepository.GetByIdAsync(discussId);
                if (discussEntity is null)
                {
                    throw new UserFriendlyException("主题为空");
                }
                discussEntity.AgreeNum += 1;
                await _discssRepository.UpdateAsync(discussEntity);

                return new AgreeDto(true);

            }
            else
            {

                //点赞过，删除即可,修改总点赞数量
                await _repository.DeleteByIdAsync(entity.Id);
                var discussEntity = await _discssRepository.GetByIdAsync(discussId);
                if (discussEntity is null)
                {
                    throw new UserFriendlyException("主题为空");
                }
                discussEntity.AgreeNum -= 1;
                await _discssRepository.UpdateAsync(discussEntity);

                return new AgreeDto(false);
            }
        }
    }
}
