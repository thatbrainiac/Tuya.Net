using Newtonsoft.Json;
using Tuya.Net.Converters;

namespace Tuya.Net.Data
{
    [JsonConverter(typeof(CategoryConverter))]
    public enum Category { 
        Cz, 
        Dj, 
        Pir,
        Unknown
    }
}
