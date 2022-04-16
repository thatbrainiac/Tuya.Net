using Tuya.Net.Api;
using Tuya.Net.Data;
using Tuya.Net.IoT;

namespace Tuya.Net
{
    /// <summary>
    /// Tuya API client interface.
    /// </summary>
    public interface ITuyaClient
    {
        /// <summary>
        /// Gets the low-level client instance.
        /// </summary>
        public ITuyaLowLevelClient LowLevel { get; }

        /// <summary>
        /// Gets the device manager.
        /// </summary>
        public IDeviceManager DeviceManager { get; }

        /// <summary>
        /// Gets the user manager.
        /// </summary>
        public IUserManager UserManager { get; }

        /// <summary>
        /// Make authenticated requests.
        /// </summary>
        /// <typeparam name="T">Object type to return.</typeparam>
        /// <param name="httpMethod">HTTP method.</param>
        /// <param name="path">API path.</param>
        /// <param name="payload">Payload string, if present.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>An instance of <see cref="T"/> containing the requested data.</returns>
        public Task<T?> RequestAsync<T>(HttpMethod httpMethod, string path, string payload = "", CancellationToken cancellationToken = default);
    }
}
