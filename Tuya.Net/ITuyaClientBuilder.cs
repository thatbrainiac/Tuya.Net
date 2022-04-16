using Microsoft.Extensions.Logging;
using Tuya.Net.Data.Settings;

namespace Tuya.Net
{
    /// <summary>
    /// Tuya Client Builder interface.
    /// </summary>
    public interface ITuyaClientBuilder
    {
        /// <summary>
        /// Specify the data center to use.
        /// </summary>
        /// <param name="dataCenter"><see cref="DataCenter"/> where the API is located.</param>
        /// <returns>An <see cref="ITuyaClientBuilder"/> instance.</returns>
        public ITuyaClientBuilder UsingDataCenter(DataCenter dataCenter);

        /// <summary>
        /// Specify the data center to use.
        /// </summary>
        /// <param name="credentials"><see cref="ITuyaCredentials"/> for authentication.</param>
        /// <returns>An <see cref="ITuyaClientBuilder"/> instance.</returns>
        public ITuyaClientBuilder UsingCredentials(ITuyaCredentials credentials);

        /// <summary>
        /// Specify the data center to use.
        /// </summary>
        /// <param name="logger"><see cref="DataCenter"/> where the API is located.</param>
        /// <returns>An <see cref="ITuyaClientBuilder"/> instance.</returns>
        public ITuyaClientBuilder UsingLogger(ILogger<ITuyaClient> logger);

        /// <summary>
        /// Specify how many times to retry authentication before throwing an exception.
        /// </summary>
        /// <param name="maxAuthRetryCount">Maximum auth retry count.</param>
        /// <returns>An <see cref="ITuyaClientBuilder"/> instance.</returns>
        public ITuyaClientBuilder MaxAuthRetryCount(int maxAuthRetryCount);

        /// <summary>
        /// Build the instance.
        /// </summary>
        /// <returns>An <see cref="ITuyaClient"/> instance.</returns>
        public ITuyaClient Build();
    }
}
