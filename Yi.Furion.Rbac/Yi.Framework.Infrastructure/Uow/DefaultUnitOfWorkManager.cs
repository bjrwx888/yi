namespace Yi.Framework.Infrastructure.Uow
{
    internal class DefaultUnitOfWorkManager : IUnitOfWorkManager
    {
        public IUnitOfWork CreateContext(bool isTran = true)
        {
            return new DefaultUnitOfWork();
        }
    }
}
