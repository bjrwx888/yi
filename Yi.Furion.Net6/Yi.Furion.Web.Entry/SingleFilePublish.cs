using Furion;
using System.Reflection;

namespace Yi.Furion.Web.Entry;

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
            "Yi.Framework.Infrastructure",
            "Yi.Framework.Module",
            "Yi.Furion.Application",
            "Yi.Furion.Core",
            "Yi.Furion.Sqlsugar.Core",
            "Yi.Furion.Core"
        };
    }
}