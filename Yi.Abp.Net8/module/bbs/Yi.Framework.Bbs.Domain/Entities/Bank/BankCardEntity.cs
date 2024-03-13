using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Yi.Framework.Bbs.Domain.Shared.Enums;

namespace Yi.Framework.Bbs.Domain.Entities.Bank
{
    /// <summary>
    /// 银行卡
    /// </summary>
    [SugarTable("BankCard")]
    public class BankCardEntity : Entity<Guid>, IHasCreationTime
    {
        [SugarColumn(ColumnName = "Id", IsPrimaryKey = true)]
        public override Guid Id { get; protected set; }
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 当前存储的钱
        /// </summary>
        public decimal StorageMoney { get; set; } = 0;


        /// <summary>
        /// 最大可存储的钱钱
        /// </summary>
        public decimal MaxStorageMoney { get; set; } = 100;


        /// <summary>
        /// 满期限时间，可空
        /// </summary>
        public DateTime? Fullterm { get; set; }


        

        /// <summary>
        /// 银行卡状态
        /// </summary>
        public BankCardStateEnum BankCardState { get; set; } = BankCardStateEnum.Unused;


    }
}
