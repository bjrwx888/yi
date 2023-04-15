
namespace Yi.Framework.Data.Auditing;


public interface IHasModificationTime
{

    DateTime? LastModificationTime { get; }
}
