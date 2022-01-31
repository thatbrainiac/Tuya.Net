using Newtonsoft.Json;
using Tuya.Net.Converters;

namespace Tuya.Net.Data
{
    /// <summary>
    /// Tuya Device Category Enum.
    /// </summary>
    [JsonConverter(typeof(CategoryConverter))]
    public enum Category { 
        Cz, 
        Dj, 
        Pir,
        Unknown
    }
}
