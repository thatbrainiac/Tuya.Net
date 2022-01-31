using Newtonsoft.Json;

namespace Tuya.Net.Data
{
    public partial class AccessTokenInfo
    {
        [JsonProperty("access_token", NullValueHandling = NullValueHandling.Ignore)]
        public string? TokenString { get; set; }

        [JsonProperty("expire_time", NullValueHandling = NullValueHandling.Ignore)]
        public long? ExpireTime { get; set; }

        [JsonProperty("refresh_token", NullValueHandling = NullValueHandling.Ignore)]
        public string? RefreshToken { get; set; }

        [JsonProperty("uid", NullValueHandling = NullValueHandling.Ignore)]
        public string? Uid { get; set; }
    }
}
