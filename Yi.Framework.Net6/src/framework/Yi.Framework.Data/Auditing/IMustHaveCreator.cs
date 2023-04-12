using System;

namespace Yi.Framework.Data.Auditing;

public interface IMustHaveCreator<TCreator> : IMustHaveCreator
{
    TCreator Creator { get; }
}

public interface IMustHaveCreator
{
    long CreatorId { get; }
}
