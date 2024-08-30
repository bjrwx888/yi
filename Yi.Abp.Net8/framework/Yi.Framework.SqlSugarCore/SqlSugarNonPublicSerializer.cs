using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SqlSugar;

namespace Yi.Framework.SqlSugarCore;

public class NonPublicPropertiesResolver : DefaultContractResolver
{
    /// <summary>
    /// 重写获取属性，存在get set方法就可以写入
    /// </summary>
    /// <param name="member"></param>
    /// <param name="memberSerialization"></param>
    /// <returns></returns>
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var prop = base.CreateProperty(member, memberSerialization);
        if (member is PropertyInfo pi)
        {
            prop.Readable = (pi.GetMethod != null);
            prop.Writable = (pi.SetMethod != null);
        }

        return prop;
    }
}

public class SqlSugarNonPublicSerializer : ISerializeService
{
    public string SerializeObject(object value)
    {
        if (value != null && value.GetType().FullName.StartsWith("System.Text.Json.")) 
        {
            // 动态创建一个 JsonSerializer 实例
            Type serializerType = value.GetType().Assembly.GetType("System.Text.Json.JsonSerializer");

            var methods =  serializerType
                .GetMyMethod("Serialize", 2);

            // 调用 SerializeObject 方法序列化对象
            string json = (string)methods.MakeGenericMethod(value.GetType())
                .Invoke(null, new object[] { value,null });
            return json;
        }
        return JsonConvert.SerializeObject(value);
    }

    public string SugarSerializeObject(object value)
    {
        return JsonConvert.SerializeObject(value, new JsonSerializerSettings()
        {
            ContractResolver = new MyContractResolver()
        });
    }
 
    public T DeserializeObject<T>(string value)
    {
        if (typeof(T).FullName.StartsWith("System.Text.Json."))
        {
            // 动态创建一个 JsonSerializer 实例
            Type serializerType =typeof(T).Assembly.GetType("System.Text.Json.JsonSerializer");

            var methods = serializerType
                .GetMethods().Where(it=>it.Name== "Deserialize")
                .Where(it=>it.GetParameters().Any(z=>z.ParameterType==typeof(string))).First();

            // 调用 SerializeObject 方法序列化对象
            T json = (T)methods.MakeGenericMethod(typeof(T))
                .Invoke(null, new object[] { value, null });
            return json;
        }
        var jSetting = new JsonSerializerSettings 
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver =new NonPublicPropertiesResolver() //提换默认解析器
        };
        return JsonConvert.DeserializeObject<T>(value, jSetting);
    }
}