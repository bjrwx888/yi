using System;

namespace Yi.Framework.Infrastructure.Data.Auditing;


public interface IHasCreationTime
{

    DateTime CreationTime { get; set; }
}
