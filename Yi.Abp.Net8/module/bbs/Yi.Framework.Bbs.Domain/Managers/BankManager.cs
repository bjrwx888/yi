using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;
using Yi.Framework.Bbs.Domain.Entities.Bank;
using Yi.Framework.Bbs.Domain.Shared.Enums;
using Yi.Framework.Bbs.Domain.Shared.Etos;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.Bbs.Domain.Managers
{
    public class BankManager : DomainService
    {
        private ISqlSugarRepository<BankCardEntity> _repository;
        private ILocalEventBus _localEventBus;
        private ISqlSugarRepository<InterestRecordsEntity> _interestRepository;
        public BankManager(ISqlSugarRepository<BankCardEntity> repository, ILocalEventBus localEventBus, ISqlSugarRepository<InterestRecordsEntity> interestRepository)
        {
            _repository = repository;
            _localEventBus = localEventBus;
            _interestRepository = interestRepository;
        }

        public decimal CurrentInterestRate => GetCurrentInterestRate();
        private decimal GetCurrentInterestRate()
        {
            //先判断时间是否与当前时间差1小时，小于1小时直接返回即可,可以由一个单例类提供
            GetThirdPartyValue();
            return 1.30m;
        }

        /// <summary>
        /// 获取第三方的值
        /// </summary>
        /// <returns></returns>
        private decimal GetThirdPartyValue()
        {
            return 0;
        }

        /// <summary>
        /// 创建一个记录
        /// </summary>
        /// <returns></returns>
        public async Task<InterestRecordsEntity> CreateInterestRecordsAsync()
        {
            //获取最新的实体
            var newEntity = await _interestRepository._DbQueryable.OrderByDescending(x => x.CreationTime).FirstAsync();
            decimal oldValue = 1.3m;
            if (newEntity is not null)
            {
                oldValue = newEntity.Value;
            }
            var currentValue = GetThirdPartyValue();
            var entity = new InterestRecordsEntity(currentValue, false, oldValue);
            var output = await _interestRepository.InsertReturnEntityAsync(entity);

            return output;
        }

        /// <summary>
        /// 给用户申请银行卡
        /// </summary>
        /// <returns></returns>
        public async Task ApplyingBankCardAsync(Guid userId, int cardNumber)
        {
            var entities = Enumerable.Range(1, cardNumber).Select(x => new BankCardEntity(userId)).ToList();
            await _repository.InsertManyAsync(entities);
        }

        /// <summary>
        /// 进行银行卡提款
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public async Task DrawMoneyAsync(Guid cardId)
        {
            var entity = await _repository.GetByIdAsync(cardId);
            if (entity.BankCardState == BankCardStateEnum.Unused)
            {
                throw new UserFriendlyException("当前银行卡状态不能提款");
            }

            //这里其实不存在这个状态，只有等待状态，不需要去主动触发，前端判断即可
            if (entity.BankCardState == BankCardStateEnum.Full)
            {
                throw new UserFriendlyException("当前银行卡状态不能存款");
            }

            //可以提款
            if (entity.BankCardState == BankCardStateEnum.Wait)
            {
                decimal changeMoney = 0;
                //判断是否存满时间
                if (entity.IsStorageFull())
                {
                    changeMoney = this.CurrentInterestRate * entity.StorageMoney;
                }
                else
                {
                    changeMoney = entity.StorageMoney;
                }

                //提款
                entity.SetDrawMoney();
                await _repository.UpdateAsync(entity);

                //打钱，该卡状态钱更新，并提款加到用户钱钱里
                await _localEventBus.PublishAsync(new MoneyChangeEventArgs(entity.UserId, changeMoney));



            }
        }

        /// <summary>
        /// 给银行卡存款
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="moneyNum"></param>
        /// <returns></returns>
        public async Task DepositAsync(Guid cardId, decimal moneyNum)
        {
            var entity = await _repository.GetByIdAsync(cardId);
            if (entity.BankCardState != BankCardStateEnum.Unused)
            {
                throw new UserFriendlyException("当前银行卡状态不能存款");
            }
            //存款
            entity.SetStorageMoney(moneyNum);

            await _repository.UpdateAsync(entity);
            await _localEventBus.PublishAsync(new MoneyChangeEventArgs(entity.UserId, -moneyNum));

        }

    }
}
