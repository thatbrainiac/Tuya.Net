namespace Tuya.Net.Security
{
    /// <summary>
    /// Tuya credentials interface.
    /// </summary>
    public interface ITuyaCredentials
    {
        /// <summary>
        /// Tuya Client id.
        /// </summary>
        string ClientId { get; }

        /// <summary>
        /// Tuya Client secret.
        /// </summary>
        string ClientSecret { get; }
    }
}
