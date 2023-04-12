using Furion;
using System.Reflection;

namespace Yi.Furion.Rbac.Web.Entry;

public class SingleFilePublish : ISingleFilePublish
{
    public Assembly[] IncludeAssemblies()
    {
        return Array.Empty<Assembly>();
    }

    public string[] IncludeAssemblyNames()
    {
        return new[]
        {
            "Yi.Furion.Rbac.Application",
            "Yi.Furion.Rbac.Core",
            "Yi.Furion.Rbac.EntityFramework.Core",
            "Yi.Furion.Rbac.Web.Core"
        };
    }
}