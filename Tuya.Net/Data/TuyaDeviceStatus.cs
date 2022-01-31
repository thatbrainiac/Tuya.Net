using Newtonsoft.Json;

namespace Tuya.Net.Data
{
    /// <summary>
    /// Tuya Device Status DTO.
    /// </summary>
    public class TuyaDeviceStatus
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string? Code { get; set; }

        /// <summary>
        /// Gets or sets the status value.
        /// </summary>
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string? Value { get; set; }
    }
}
