namespace Yi.Framework.Uow
{
    public interface IUnitOfWorkManager
    {
        IUnitOfWork CreateContext(bool isTran = true);
    }

}