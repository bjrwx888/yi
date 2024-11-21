using System.Collections.Specialized;

namespace Yi.Framework.Core.Helper;

/// <summary>
/// 数据装配帮助类
/// </summary>
public static class AssemblyDataHelper
{
    /// <summary>
    /// 装配数据
    /// </summary>
    /// <param name="dataList">数据</param>
    /// <param name="keyValues">装配键值，键为原始数据列，值为目标列</param>
    /// <param name="dataDicFunc">装配的原始字典</param>
    /// <typeparam name="T"></typeparam>
    public static async Task AssemblyData<T>(List<T> dataList, NameValueCollection keyValues,
        Func<HashSet<Guid>, Task<Dictionary<Guid, string>>> dataDicFunc)
    {
        HashSet<Guid> idList = [];
        var t = typeof(T);
        foreach (var data in dataList)
        {
            foreach (var m in keyValues.AllKeys)
            {
                var p = t.GetProperty(m);
                if (p != null)
                {
                    var v = p.GetValue(data);
                    if (v is Guid guid)
                    {
                        idList.Add(guid);
                    }

                    continue;
                }

                var f = t.GetField(m);
                if (f != null)
                {
                    var v = f.GetValue(data);
                    if (v is Guid g)
                    {
                        idList.Add(g);
                    }
                }
            }
        }

        var userDic = await dataDicFunc.Invoke(idList);
        foreach (var data in dataList)
        {
            foreach (var m in keyValues.AllKeys)
            {
                var p = t.GetProperty(m);
                if (p != null)
                {
                    var v = p.GetValue(data);
                    if (v is Guid guid)
                    {
                        var k = t.GetProperty(keyValues[m]);
                        var val = userDic[guid];
                        k.SetValue(data, val ?? v);
                    }

                    continue;
                }

                var f = t.GetField(m);
                if (f != null)
                {
                    var v = f.GetValue(data);
                    if (v is Guid guid1)
                    {
                        var k = t.GetField(keyValues[m]);
                        var value = userDic[guid1];
                        k.SetValue(data, value ?? v);
                    }
                }
            }
        }
    }
}