using Newtonsoft.Json;

namespace Tuya.Net.Data
{
    public class TuyaUser
    {
        [JsonProperty("avatar")]
        public Uri? AvatarUrl { get; set; }

        [JsonProperty("country_code")]
        public string? CountryCode { get; set; }

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
