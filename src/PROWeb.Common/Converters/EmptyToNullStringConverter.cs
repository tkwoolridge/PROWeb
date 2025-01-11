using System.Text.Json;
using System.Text.Json.Serialization;

namespace PROWeb.Common.Converters
{
    public class EmptyToNullStringConverter : JsonConverter<string?>
    {
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = reader.GetString();

            if (string.IsNullOrEmpty(value))
            {
                value = null;
            }

            return value;
        }

        public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}
