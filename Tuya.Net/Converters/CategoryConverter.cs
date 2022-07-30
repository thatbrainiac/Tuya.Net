using Newtonsoft.Json;
using Tuya.Net.Data;

namespace Tuya.Net.Converters
{
    /// <summary>
    /// DeviceCategory converter.
    /// </summary>
    public class CategoryConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(DeviceCategory) || t == typeof(DeviceCategory?);

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return DeviceCategory.Unknown;
            }
            var value = serializer.Deserialize<string>(reader);
            return value switch
            {
                "cz" => DeviceCategory.Cz,
                "dj" => DeviceCategory.Dj,
                "pir" => DeviceCategory.Pir,
                "infrared_ac" => DeviceCategory.AirConditioning,
                _ => DeviceCategory.Unknown,
            };
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }

            string valueString;
            var value = (DeviceCategory)untypedValue;

            switch (value)
            {
                case DeviceCategory.Cz:
                    valueString = "cz";
                    break;
                case DeviceCategory.Dj:
                    valueString = "dj";
                    break;
                case DeviceCategory.Pir:
                    valueString = "pir";
                    break;
                default:
                    valueString = "unknown";
                    break;
            }

            serializer.Serialize(writer, valueString);
        }
    }
}
