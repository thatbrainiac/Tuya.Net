using Newtonsoft.Json;

namespace Tuya.Net.Data
{
    /// <summary>
    /// Tuya user DTO.
    /// </summary>
    public class TuyaUser
    {
        /// <summary>
        /// Gets or sets the avatar url.
        /// </summary>
        [JsonProperty("avatar")]
        public Uri? AvatarUrl { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        [JsonProperty("country_code")]
        public string? CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        [JsonProperty("create_time")]
        public long? CreateTime { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("mobile")]
        public string? Mobile { get; set; }

        [JsonProperty("nick_name")]
        public string? Nickname { get; set; }

        [JsonProperty("temp_unit")]
        public long TempUnit { get; set; }

        [JsonProperty("time_zone_id")]
        public string? TimeZoneId { get; set; }

        [JsonProperty("uid")]
        public string? Uid { get; set; }

        [JsonProperty("update_time")]
        public long? UpdateTime { get; set; }

        [JsonProperty("username")]
        public string? Username { get; set; }
    }
}
