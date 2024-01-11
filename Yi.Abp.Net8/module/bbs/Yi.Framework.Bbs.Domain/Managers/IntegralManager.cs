using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;
using Yi.Framework.Bbs.Domain.Entities.Integral;
using Yi.Framework.Bbs.Domain.Shared.Etos;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.Bbs.Domain.Managers
{
    public class IntegralManager : DomainService
    {
        private ISqlSugarRepository<LevelEntity> _levelRepository;
        private ISqlSugarRepository<SignInEntity> _signInRepository;
        private readonly ILocalEventBus _localEventBus;
        public IntegralManager(ISqlSugarRepository<LevelEntity> levelRepository, ISqlSugarRepository<SignInEntity> signInRepository, ILocalEventBus localEventBus)
        {
            _levelRepository = levelRepository;
            _localEventBus = localEventBus;
            _signInRepository = signInRepository;
        }


        /// <summary>
        /// 签到
        /// </summary>
        /// <returns></returns>
        public async Task PostSignInAsync(Guid userId)
        {
            //签到，添加用户钱钱
            //发送一个充值的领域事件即可


            //签到添加的钱钱，跟连续签到有关系
            //每天随机（3-10），连续签到每次累加多1点，最多一天30

            //额外
            //如果随机数数字都相同，额外再获取乘10倍

            //这种逻辑，就是属于核心领域业务了
            decimal number = 3;


            //发布一个其他领域的事件
            await _localEventBus.PublishAsync(new MoneyChangeEventArgs() { UserId = userId, Number = number });
        }



    }
}
