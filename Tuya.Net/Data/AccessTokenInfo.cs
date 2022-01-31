using Newtonsoft.Json;

namespace Tuya.Net.Data
{
    /// <summary>
    /// Access Token Info DTO.
    /// </summary>
    public class AccessTokenInfo
    {
        /// <summary>
        /// Gets or sets the token string.
        /// </summary>
        [JsonProperty("access_token", NullValueHandling = NullValueHandling.Ignore)]
        public string? TokenString { get; set; }

        /// <summary>
        /// Gets or sets the expiration time.
        /// </summary>
        [JsonProperty("expire_time", NullValueHandling = NullValueHandling.Ignore)]
        public long? ExpireTime { get; set; }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        [JsonProperty("refresh_token", NullValueHandling = NullValueHandling.Ignore)]
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the UID.
        /// </summary>
        [JsonProperty("uid", NullValueHandling = NullValueHandling.Ignore)]
        public string? Uid { get; set; }
    }
}
