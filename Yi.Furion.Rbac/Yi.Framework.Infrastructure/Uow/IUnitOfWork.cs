using Yi.Framework.Infrastructure.Ddd.Repositories;

namespace Yi.Framework.Infrastructure.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        bool IsTran { get; set; }
        bool IsCommit { get; set; }
        bool IsClose { get; set; }

        IRepository<T> GetRepository<T>();
        bool Commit();
    }
}
