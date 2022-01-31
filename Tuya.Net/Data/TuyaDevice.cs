using Newtonsoft.Json;

namespace Tuya.Net.Data
{
    /// <summary>
    /// Tuya Device.
    /// </summary>
    public class TuyaDevice : TuyaDeviceInfo
    {
        /// <summary>
        /// Gets or sets the Tuya Device status list.
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public IList<TuyaDeviceStatus>? Status { get; set; }
    }
}
