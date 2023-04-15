using System;

namespace Yi.Framework.Infrastructure.Data.Auditing;

public interface IMustHaveCreator<TCreator> : IMustHaveCreator
{
    TCreator Creator { get; }
}

public interface IMustHaveCreator
{
    long CreatorId { get; }
}
