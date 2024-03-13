using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Yi.Framework.Bbs.Domain.Managers;

namespace Yi.Framework.Bbs.Application.Services.Bank
{
    public class BankService : ApplicationService
    {
        private BankManager _bankManager;
        public BankService(BankManager bankManager)
        {
            _bankManager = bankManager;
        }

        /// <summary>
        /// 给用户申请银行卡
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public Task ApplyingBankCardAsync()
        {
            return _bankManager.ApplyingBankCardAsync(CurrentUser.Id.Value);
        }

        /// <summary>
        /// 给银行卡提款
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        [Authorize]
        public Task DrawMoneyAsync(Guid cardId)
        {
            return _bankManager.DrawMoneyAsync(cardId);
        }
        /// <summary>
        /// 给银行卡存款
        /// </summary>
        /// <param name="CardId"></param>
        /// <param name="moneyNum"></param>
        /// <returns></returns>
        public Task DepositAsync(Guid CardId, decimal moneyNum)
        {
            return _bankManager.DepositAsync(CardId, moneyNum);
        }


    }
}
