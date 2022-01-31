using Newtonsoft.Json;

namespace Tuya.Net.Data
{
    public class TuyaDeviceStatus
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string? Code { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string? Value { get; set; }
    }
}
