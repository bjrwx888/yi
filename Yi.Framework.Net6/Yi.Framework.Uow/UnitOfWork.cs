using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Uow
{
    public class UnitOfWork : IUnitOfWork
    {

        public bool IsTransactional { get; protected set; }

        public IsolationLevel? IsolationLevel { get; protected set; }

        /// <summary>
        /// Milliseconds
        /// </summary>
        public int? Timeout { get; protected set; }

        public void Init(bool isTransactional, IsolationLevel? isolationLevel, int? timeout)
        {
            IsTransactional = isTransactional;
            IsolationLevel = isolationLevel;
            Timeout = timeout;
        }

        public ISqlSugarClient SugarClient { get; set; }
        /// <summary>
        /// 因为sqlsugarclient的生命周期是作用域的，也就是说一个请求线程内是共用一个client，暂时先直接注入
        /// </summary>
        /// <param name="sqlSugarClient"></param>
        public UnitOfWork(ISqlSugarClient sqlSugarClient)
        {
            this.SugarClient = sqlSugarClient;
        }


        public void Dispose()
        {
            SugarClient?.Dispose();
            SugarClient?.Close();
        }



        public void BeginTran()
        {
            if (IsTransactional)
            {
                if (IsolationLevel.HasValue)
                {
                    SugarClient.Ado.BeginTran(IsolationLevel.Value);
                }
                else
                {
                    SugarClient.Ado.BeginTran();
                }
            }
        }

        public void CommitTran()
        {
            if (IsTransactional)
                SugarClient.Ado.CommitTran();
        }
        public void RollbackTran()
        {
            if (IsTransactional)
                SugarClient.Ado.RollbackTran();
        }
    }
}
