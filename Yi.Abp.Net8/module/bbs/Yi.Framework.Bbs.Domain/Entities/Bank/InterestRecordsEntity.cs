using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace Yi.Framework.Bbs.Domain.Entities.Bank
{
    /// <summary>
    /// 利息记录
    /// </summary>
    [SugarTable("InterestRecords")]
    public class InterestRecordsEntity : Entity<Guid>, IHasCreationTime
    {
        public InterestRecordsEntity()
        { }
        public InterestRecordsEntity(decimal inputValue, bool isFluctuate, decimal oldValue = 0)
        {
            //这里写好根据数据的值，以及是否要波动期，进行得出真是利息
            //有了老值和新值，我们可以根据这个变化程度去做一个涨幅或跌幅，Todo
            Value=inputValue;
        }
        [SugarColumn(ColumnName = "Id", IsPrimaryKey = true)]
        public override Guid Id { get; protected set; }
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 当前汇率值
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// 是否波动期
        /// </summary>
        public bool IsFluctuate { get; set; }


    }
}
