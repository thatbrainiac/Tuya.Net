using Newtonsoft.Json;

namespace Tuya.Net.Data
{
    /// <summary>
    /// Tuya user DTO.
    /// </summary>
    public class TuyaUser
    {
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        [JsonProperty("username")]
        public string? Name { get; set; }

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

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        [JsonProperty("email")]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the user mobile.
        /// </summary>
        [JsonProperty("mobile")]
        public string? Mobile { get; set; }

        /// <summary>
        /// Gets or sets the nickname.
        /// </summary>
        [JsonProperty("nick_name")]
        public string? Nickname { get; set; }

        /// <summary>
        /// Gets or sets the temperature unit.
        /// </summary>
        [JsonProperty("temp_unit")]
        public long TempUnit { get; set; }

        /// <summary>
        /// Gets or sets the time zone id.
        /// </summary>
        [JsonProperty("time_zone_id")]
        public string? TimeZoneId { get; set; }

        /// <summary>
        /// Gets or sets the UID.
        /// </summary>
        [JsonProperty("uid")]
        public string? Uid { get; set; }

        /// <summary>
        /// Gets or sets the update time.
        /// </summary>
        [JsonProperty("update_time")]
        public long? UpdateTime { get; set; }
    }
}
