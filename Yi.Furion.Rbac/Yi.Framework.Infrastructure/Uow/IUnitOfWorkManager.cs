namespace Yi.Framework.Infrastructure.Uow
{
    public interface IUnitOfWorkManager
    {
        IUnitOfWork CreateContext(bool isTran = true);
    }

}