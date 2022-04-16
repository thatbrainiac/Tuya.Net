using Microsoft.Extensions.Logging;
using Tuya.Net.Data.Settings;
using Tuya.Net.Exceptions;

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
        /// Specify the Tuya API client id.
        /// </summary>
        /// <param name="clientId">The Tuya client id.</param>
        /// <returns>An <see cref="ITuyaClientBuilder"/> instance.</returns>
        public ITuyaClientBuilder UsingClientId(string clientId);

        /// <summary>
        /// Specify the Tuya API client secret.
        /// </summary>
        /// <param name="clientSecret">The Tuya client secret.</param>
        /// <returns>An <see cref="ITuyaClientBuilder"/> instance.</returns>
        public ITuyaClientBuilder UsingSecret(string clientSecret);

        /// <summary>
        /// Specify the logger instance to use for logging.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
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
        /// <exception cref="TuyaClientBuilderException">Thrown when there the client failed to build due to a missing build component.</exception>
        public ITuyaClient Build();
    }
}
