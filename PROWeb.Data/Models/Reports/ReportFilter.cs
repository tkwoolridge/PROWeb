using PROWeb.Data.Models.Enums;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PROWeb.Data.Models.Reports
{
    [XmlRoot(ElementName = "Filter", Namespace = "http://schemas.datacontract.org/2004/07/PROWeb.Data.Models.Reports")]
    public class ReportFilter : IXmlSerializable, IReportFilter
    {
        [XmlAttribute]
        public string? Name { get; set; }

        [XmlAttribute]
        [JsonConverter(typeof(JSONConverterForType))]
        public Type? FilterType { get; set; }

        [XmlAttribute]
        [JsonConverter(typeof(JSONConverterForObject))]
        public object? Value { get; set; }

        [XmlAttribute]
        public ReportFilterOperator Operator { get; set; }

        public XmlSchema? GetSchema()
        {
            return null;
        }

        public virtual void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            Name = reader.GetAttribute(nameof(Name));
            Value = reader.GetAttribute(nameof(Value));

            if (reader.GetAttribute(nameof(FilterType)) is { } typeName)
            {
                FilterType = Type.GetType(typeName);
            }

            if (Enum.TryParse(reader.GetAttribute(nameof(Operator)), out ReportFilterOperator @operator))
            {
                Operator = @operator;
            }

            if (reader.ReadToFollowing(nameof(Value)) && FilterType is { } type)
            {
                DataContractSerializer serializer = new DataContractSerializer(type, nameof(Value), reader.NamespaceURI);
                Value = serializer.ReadObject(reader);
            }
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString(nameof(Name), Name);
            writer.WriteAttributeString(nameof(Operator), Operator.ToString());

            if (FilterType is { } type && Value is not null)
            {
                DataContractSerializer serializer = new DataContractSerializer(FilterType);

                serializer.WriteObject(writer, Value);
            }

            if (FilterType?.FullName is { } typeName)
            {
                writer.WriteAttributeString(nameof(FilterType), typeName);
            }
        }
    }

    internal class JSONConverterForType : JsonConverter<Type>
    {
        public override Type? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.GetString() is { } typeName && PredefinedFilterTypes.IsPredefinedType(typeName))
            {
                return Type.GetType(typeName);
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, Type? value, JsonSerializerOptions options)
        {
            if (value is { } type)
            {
                writer.WriteStringValue(type.FullName);
            }
        }
    }

    internal class JSONConverterForObject : JsonConverter<object>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return PredefinedFilterTypes.IsPredefinedType(typeToConvert);
        }

        public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    {
                        if (reader.TryGetDateTime(out var value10))
                        {
                            return value10;
                        }

                        if (reader.TryGetDateTimeOffset(out var value11))
                        {
                            return value11;
                        }

                        return reader.GetString();
                    }
                case JsonTokenType.Number:
                    {
                        if (reader.TryGetInt16(out var value))
                        {
                            return value;
                        }

                        if (reader.TryGetInt32(out var value2))
                        {
                            return value2;
                        }

                        if (reader.TryGetInt64(out var value3))
                        {
                            return value3;
                        }

                        if (reader.TryGetUInt16(out var value4))
                        {
                            return value4;
                        }

                        if (reader.TryGetUInt32(out var value5))
                        {
                            return value5;
                        }

                        if (reader.TryGetUInt64(out var value6))
                        {
                            return value6;
                        }

                        if (reader.TryGetDecimal(out var value7))
                        {
                            return value7;
                        }

                        if (reader.TryGetDouble(out var value8))
                        {
                            return value8;
                        }

                        if (reader.TryGetSingle(out var value9))
                        {
                            return value9;
                        }

                        return null;
                    }
                case JsonTokenType.True:
                    return true;
                case JsonTokenType.False:
                    return false;
                case JsonTokenType.Null:
                    return null;
                default:
                    return reader.GetString();
            }
        }

        public override void Write(Utf8JsonWriter writer, object? value, JsonSerializerOptions options)
        {
            if (value?.GetType() is { } type)
            {
                JsonSerializer.Serialize(writer, value, type, options);
            }
            else
            {
                JsonSerializer.Serialize(writer, value, options);
            }
        }
    }
}
