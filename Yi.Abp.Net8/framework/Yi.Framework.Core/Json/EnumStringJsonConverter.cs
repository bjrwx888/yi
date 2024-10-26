using System.Text.Json;
using System.Text.Json.Serialization;

namespace Yi.Framework.Core.Json;

public class EnumStringJsonConverter : JsonConverter<Enum>
{
    public override Enum? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var enumString = reader.GetString();
        return (Enum)Enum.Parse(typeToConvert, enumString);
    }

    public override void Write(Utf8JsonWriter writer, Enum value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}