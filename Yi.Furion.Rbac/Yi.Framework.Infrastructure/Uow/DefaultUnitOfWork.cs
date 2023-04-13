using Yi.Framework.Infrastructure.Ddd.Repositories;

namespace Yi.Framework.Infrastructure.Uow
{
    internal class DefaultUnitOfWork : IUnitOfWork
    {
        public DefaultUnitOfWork() { }
        public bool IsTran { get; set; }
        public bool IsCommit { get; set; }
        public bool IsClose { get; set; }

        public bool Commit()
        {
            return true;
        }

        public void Dispose()
        {
        }

        public IRepository<T> GetRepository<T>()
        {
            throw new NotImplementedException();
        }
    }
}
