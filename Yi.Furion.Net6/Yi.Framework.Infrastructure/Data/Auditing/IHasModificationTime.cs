namespace Yi.Framework.Infrastructure.Data.Auditing;


public interface IHasModificationTime
{

    DateTime? LastModificationTime { get; }
}
