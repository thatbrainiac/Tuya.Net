using Newtonsoft.Json;
using Tuya.Net.Data;

namespace Tuya.Net.Converters
{
    /// <summary>
    /// Category converter.
    /// </summary>
    public class CategoryConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Category) || t == typeof(Category?);

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return Category.Unknown;
            }
            var value = serializer.Deserialize<string>(reader);
            return value switch
            {
                "cz" => Category.Cz,
                "dj" => Category.Dj,
                "pir" => Category.Pir,
                _ => Category.Unknown,
            };
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Category)untypedValue;
            switch (value)
            {
                case Category.Cz:
                    serializer.Serialize(writer, "cz");
                    break;
                case Category.Dj:
                    serializer.Serialize(writer, "dj");
                    break;
                case Category.Pir:
                    serializer.Serialize(writer, "pir");
                    break;
                default:
                    serializer.Serialize(writer, "unknown");
                    break;
            }
        }

        public static readonly CategoryConverter Singleton = new CategoryConverter();
    }
}
