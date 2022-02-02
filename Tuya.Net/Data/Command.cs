using Newtonsoft.Json;

namespace Tuya.Net.Data
{
    /// <summary>
    /// Tuya Device Command DTO.
    /// </summary>
    public class Command
    {
        /// <summary>
        /// Gets or sets the command code.
        /// </summary>
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string? Code { get; set; }

        /// <summary>
        /// Gets or sets the command value.
        /// </summary>
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Value { get; set; }
    }
}
