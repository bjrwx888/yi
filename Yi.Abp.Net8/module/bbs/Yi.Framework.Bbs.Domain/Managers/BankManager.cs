using Volo.Abp.Domain.Services;
using Yi.Framework.Bbs.Domain.Entities.Bank;

namespace Yi.Framework.Bbs.Domain.Managers
{
    public class BankManager : DomainService
    {

        public BankManager() { }


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
        public InterestRecordsEntity CreateInterestRecords()
        {
            return new InterestRecordsEntity();
        }

        /// <summary>
        /// 给用户申请银行卡
        /// </summary>
        /// <returns></returns>
        public Task ApplyingBankCardAsync(Guid userId)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 给银行卡提款
        /// </summary>
        /// <param name="CardId"></param>
        /// <returns></returns>
        public Task DrawMoneyAsync(Guid CardId)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 给银行卡存款
        /// </summary>
        /// <param name="CardId"></param>
        /// <param name="moneyNum"></param>
        /// <returns></returns>
        public Task DepositAsync(Guid CardId, decimal moneyNum)
        {
            return Task.CompletedTask;
        }

    }
}
