using System;

namespace Yi.Framework.Infrastructure.Data.Auditing;

public interface IMayHaveCreator<TCreator>
{
    TCreator Creator { get; }
}

public interface IMayHaveCreator
{
    long? CreatorId { get; }
}
