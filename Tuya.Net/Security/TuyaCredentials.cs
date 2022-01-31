namespace Tuya.Net.Security
{
    public class TuyaCredentials : ITuyaCredentials
    {
        /// <inheritdoc />
        public string ClientId { get; set; } = string.Empty;

        /// <inheritdoc />
        public string ClientSecret { get; set; } = string.Empty;
    }
}
