namespace Tuya.Net.Data.Settings
{
    /// <summary>
    /// Tuya credentials.
    /// </summary>
    public class TuyaCredentials : ITuyaCredentials
    {
        /// <inheritdoc />
        public string ClientId { get; init; } = string.Empty;

        /// <inheritdoc />
        public string ClientSecret { get; init; } = string.Empty;
    }
}