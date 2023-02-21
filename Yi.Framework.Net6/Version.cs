
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyCompany("YiFramework")]
[assembly: AssemblyFileVersion(VersionInfo.FullVersion)]
[assembly: AssemblyInformationalVersion(VersionInfo.FullVersion)]
[assembly: AssemblyVersion(VersionInfo.FullVersion)]
[assembly: ComVisible(false)]

#pragma warning disable IDE1006 // 命名样式
internal static class VersionInfo
{
    /// <summary>
    /// 主版本： 项目进行了重大修改，导致不再向下兼容，主版本号加1，其它版本号清0；
    /// </summary>
    public const string Major = "1";

    /// <summary>
    /// 次版本：项目进行了重构，增加了功能，但向下兼容，子版本号加1，主版本号不变， 修正版本号清0；
    /// </summary>
    public const string Minor = "1";

    /// <summary>
    /// 生成号
    /// </summary>
    public const string Build = "0";

    /// <summary>
    /// 修订号：项目进行了Bug修复，向下兼容，修正版本号加1，主版本号、子版本号不 变；
    /// </summary>
    public const string Revision = "0";

    /// <summary>
    /// 版本名称
    /// </summary>
    public const string VersionName = null;

    /// <summary>
    /// 版本
    /// </summary>
    public const string FullVersion = Major + "." + Minor + "." + Build + "." + Revision;
#pragma warning disable CS1570 // XML 注释出现 XML 格式错误
    /// <summary>
    /// 比较 a,b两个版本
    /// 当 a > b 时，返回 1
    /// 当 a = b 时，返回 0
    /// 当 a < b 时，返回 -1
    /// </summary>
#pragma warning restore CS1570 // XML 注释出现 XML 格式错误
    public static int Compare(this Version a, Version b)

    {
        // 比较主版本
        if (a.Major != b.Major)
        {
            return a.Major > b.Major ? 1 : -1;
        }

        // 比较次版本
        if (a.Minor != b.Minor)
        {
            return a.Minor > b.Minor ? 1 : -1;
        }

        // 比较生成号
        if (a.Build < 0 || b.Build < 0)
        {
            return 0;
        }

        if (a.Build != b.Build)
        {
            return a.Build > b.Build ? 1 : -1;
        }

        // 比较修订号
        if (a.Revision < 0 || b.Revision < 0)
        {
            return 0;
        }
        if (a.Revision != b.Revision)
        {
            return a.Revision > b.Revision ? 1 : -1;
        }

        return 0;
    }
#pragma warning restore IDE1006 // 命名样式
}