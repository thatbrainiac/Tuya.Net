namespace Tuya.Net.Data.Settings
{
    /// <summary>
    /// Tuya credentials.
    /// </summary>
    public class TuyaCredentials : ITuyaCredentials
    {
        /// <inheritdoc />
        public string ClientId { get; set; } = string.Empty;

        /// <inheritdoc />
        public string ClientSecret { get; set; } = string.Empty;
    }
}