using Newtonsoft.Json;

namespace Tuya.Net.Data
{
    /// <summary>
    /// Tuya user DTO.
    /// </summary>
    public class User : IIdentifiable
    {
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the avatar url.
        /// </summary>
        [JsonProperty("avatar", NullValueHandling = NullValueHandling.Ignore)]
        public Uri? AvatarUrl { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        [JsonProperty("country_code", NullValueHandling = NullValueHandling.Ignore)]
        public string? CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        [JsonProperty("create_time", NullValueHandling = NullValueHandling.Ignore)]
        public long? CreateTime { get; set; }

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the user mobile.
        /// </summary>
        [JsonProperty("mobile", NullValueHandling = NullValueHandling.Ignore)]
        public string? Mobile { get; set; }

        /// <summary>
        /// Gets or sets the nickname.
        /// </summary>
        [JsonProperty("nick_name", NullValueHandling = NullValueHandling.Ignore)]
        public string? Nickname { get; set; }

        /// <summary>
        /// Gets or sets the temperature unit.
        /// </summary>
        [JsonProperty("temp_unit", NullValueHandling = NullValueHandling.Ignore)]
        public long TempUnit { get; set; }

        /// <summary>
        /// Gets or sets the time zone id.
        /// </summary>
        [JsonProperty("time_zone_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? TimeZoneId { get; set; }

        /// <summary>
        /// Gets or sets the User ID.
        /// </summary>
        [JsonProperty("uid", NullValueHandling = NullValueHandling.Ignore)]
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the update time.
        /// </summary>
        [JsonProperty("update_time", NullValueHandling = NullValueHandling.Ignore)]
        public long? UpdateTime { get; set; }
    }
}
