namespace Tuya.Net.Data.Settings
{
    /// <summary>
    /// Tuya credentials interface.
    /// </summary>
    public interface ITuyaCredentials
    {
        /// <summary>
        /// Gets the Tuya client id.
        /// </summary>
        string ClientId { get; init; }

        /// <summary>
        /// Gets the Tuya client secret.
        /// </summary>
        string ClientSecret { get; init; }
    }
}
