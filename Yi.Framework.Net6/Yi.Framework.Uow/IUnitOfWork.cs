using System.Data;

namespace Yi.Framework.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        public void Init(bool isTransactional, IsolationLevel? isolationLevel, int? timeout);
        public void BeginTran();

        public void CommitTran();
        public void RollbackTran();
    }
}