using Newtonsoft.Json;

namespace Tuya.Net.Data
{
    /// <summary>
    /// Tuya Device DAO.
    /// </summary>
    public class TuyaDeviceInfo
    {
        /// <summary>
        /// Gets or sets the active time in ms since Epoch.
        /// </summary>
        [JsonProperty("active_time", NullValueHandling = NullValueHandling.Ignore)]
        public long? TimeActive { get; set; }

        /// <summary>
        /// Gets or sets the category ID.
        /// </summary>
        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public string? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        [JsonProperty("category_name", NullValueHandling = NullValueHandling.Ignore)]
        public string? CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the create time.
        /// </summary>
        [JsonProperty("create_time", NullValueHandling = NullValueHandling.Ignore)]
        public long? TimeCreated { get; set; }

        /// <summary>
        /// Gets or sets the gateway id.
        /// </summary>
        [JsonProperty("gateway_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? GatewayId { get; set; }

        /// <summary>
        /// Gets or sets the icon url (relative).
        /// </summary>
        [JsonProperty("icon", NullValueHandling = NullValueHandling.Ignore)]
        public string? Icon { get; set; }

        /// <summary>
        /// Gets or sets the device ID.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the device IP address.
        /// </summary>
        [JsonProperty("ip", NullValueHandling = NullValueHandling.Ignore)]
        public string? Ip { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        [JsonProperty("lat", NullValueHandling = NullValueHandling.Ignore)]
        public string? Lat { get; set; }

        /// <summary>
        /// Gets or sets the local key.
        /// </summary>
        [JsonProperty("local_key", NullValueHandling = NullValueHandling.Ignore)]
        public string? LocalKey { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        [JsonProperty("lon", NullValueHandling = NullValueHandling.Ignore)]
        public string? Lon { get; set; }

        /// <summary>
        /// Gets or sets the model id.
        /// </summary>
        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public string? Model { get; set; }

        /// <summary>
        /// Gets or sets the device name.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the online status.
        /// </summary>
        [JsonProperty("online", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsOnline { get; set; }

        /// <summary>
        /// Gets or sets the owner ID.
        /// </summary>
        [JsonProperty("owner_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        [JsonProperty("product_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        [JsonProperty("product_name", NullValueHandling = NullValueHandling.Ignore)]
        public string? ProductName { get; set; }

        /// <summary>
        /// Gets or sets the sub device status.
        /// </summary>
        [JsonProperty("sub", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsSubDevice { get; set; }

        /// <summary>
        /// Gets or sets the timezone offset.
        /// </summary>
        [JsonProperty("time_zone", NullValueHandling = NullValueHandling.Ignore)]
        public string? TimeZoneOffset { get; set; }

        /// <summary>
        /// Gets or sets the last updated time.
        /// </summary>
        [JsonProperty("update_time", NullValueHandling = NullValueHandling.Ignore)]
        public long? TimeUpdated { get; set; }

        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        [JsonProperty("uuid", NullValueHandling = NullValueHandling.Ignore)]
        public string? Uuid { get; set; }
    }
}
