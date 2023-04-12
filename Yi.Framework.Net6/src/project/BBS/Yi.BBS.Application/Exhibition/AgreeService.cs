using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cike.AutoWebApi.Setting;
using Yi.BBS.Application.Contracts.Exhibition.Dtos.Argee;
using Yi.BBS.Domain.Exhibition.Entities;
using Yi.BBS.Domain.Forum.Entities;
using Yi.Framework.Core.CurrentUsers;
using Yi.Framework.Ddd.Repositories;
using Yi.Framework.Ddd.Services;
using Yi.Framework.Ddd.Services.Abstract;
using Yi.Framework.Uow;

namespace Yi.BBS.Application.Exhibition
{
    /// <summary>
    /// 点赞功能
    /// </summary>
    [AppService]
    public class AgreeService : ApplicationService, IApplicationService, IAutoApiService
    {

        [Autowired]
        private IRepository<AgreeEntity> _repository { get; set; }

        [Autowired]
        private IRepository<DiscussEntity> _discssRepository { get; set; }
        [Autowired]
        private ICurrentUser _currentUser { get; set; }

        [Autowired]
        private IUnitOfWorkManager _unitOfWorkManager { get; set; }

        /// <summary>
        /// 点赞,返回true为点赞+1，返回false为点赞-1
        /// </summary>
        /// <returns></returns>
        public async Task<AgreeDto> PostOperateAsync(long discussId)
        {
            var entity = await _repository.GetFirstAsync(x => x.DiscussId == discussId && x.CreatorId == _currentUser.Id);
            //判断是否已经点赞过
            if (entity is null)
            {
                using (var uow = _unitOfWorkManager.CreateContext())
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
                    uow.Commit();
                }
                return new AgreeDto(true);

            }
            else
            {
                using (var uow = _unitOfWorkManager.CreateContext())
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
                    uow.Commit();
                }

                return new AgreeDto(false);
            }
        }
    }
}
