using System.Text.Json;
using System.Text.Json.Serialization;

namespace PROWeb.Data.Models.Reports
{
    [JsonDerivedType(typeof(ReportFilter))]
    [JsonDerivedType(typeof(ReportFilterGroup))]
    [JsonConverter(typeof(ReportFilterConverter))]
    public interface IReportFilter
    {
    }

    internal class ReportFilterConverter : JsonConverter<IReportFilter>
    {
        public override IReportFilter? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);
            string propertyName = ConvertPropertyName(options, "LogicalOperator");
            if (jsonDocument.RootElement.TryGetProperty(propertyName, out var _))
            {
                return JsonSerializer.Deserialize<ReportFilterGroup>(jsonDocument.RootElement.GetRawText(), options);
            }

            return JsonSerializer.Deserialize<ReportFilter>(jsonDocument.RootElement.GetRawText(), options);
        }

        public override void Write(Utf8JsonWriter writer, IReportFilter value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }

        private static string ConvertPropertyName(JsonSerializerOptions options, string propertyName)
        {
            return options.PropertyNamingPolicy?.ConvertName(propertyName) ?? propertyName;
        }
    }
}
