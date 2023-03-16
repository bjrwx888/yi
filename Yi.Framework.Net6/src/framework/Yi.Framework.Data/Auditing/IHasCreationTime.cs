using System;

namespace Yi.Framework.Data.Auditing;


public interface IHasCreationTime
{

    DateTime CreationTime { get; set; }
}
