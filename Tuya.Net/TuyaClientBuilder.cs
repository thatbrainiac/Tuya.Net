using Microsoft.Extensions.Logging;
using Tuya.Net.Data.Settings;
using Tuya.Net.Exceptions;

namespace Tuya.Net
{
    /// <summary>
    /// Tuya client builder class.
    /// </summary>
    public sealed class TuyaClientBuilder : ITuyaClientBuilder
    {
        /// <summary>
        /// Tuya data center.
        /// </summary>
        private DataCenter dataCenter = DataCenter.Unknown;

        /// <summary>
        /// Tuya credentials.
        /// </summary>
        private readonly ITuyaCredentials credentials = new TuyaCredentials();
        
        /// <summary>
        /// Logger instance.
        /// </summary>
        private ILogger<ITuyaClient>? logger;

        /// <summary>
        /// Max auth retry count.
        /// </summary>
        private int maxAuthRetryCount = 3;

        /// <inheritdoc />
        public ITuyaClientBuilder UsingDataCenter(DataCenter tuyaDataCenter)
        {
            dataCenter = tuyaDataCenter;
            return this;
        }

        /// <inheritdoc />
        public ITuyaClientBuilder UsingClientId(string clientId)
        {
            credentials.ClientId = clientId;
            return this;
        }

        /// <inheritdoc />
        public ITuyaClientBuilder UsingSecret(string clientSecret)
        {
            credentials.ClientSecret = clientSecret;
            return this;
        }

        /// <inheritdoc />
        public ITuyaClientBuilder UsingLogger(ILogger<ITuyaClient> clientLogger)
        {
            logger = clientLogger;
            return this;
        }

        /// <inheritdoc />
        public ITuyaClientBuilder MaxAuthRetryCount(int maxRetryCount)
        {
            maxAuthRetryCount = maxRetryCount;
            return this;
        }

        /// <inheritdoc />
        public ITuyaClient Build()
        {
            if (dataCenter == DataCenter.Unknown)
            {
                throw new TuyaClientBuilderException("No data center has been provided. Please provide a data center first before building the client.");
            }

            if (string.IsNullOrEmpty(credentials.ClientId))
            {
                throw new TuyaClientBuilderException("The client id is empty or is missing. Please specify the client id before building the client.");
            }

            if (string.IsNullOrEmpty(credentials.ClientSecret))
            {
                throw new TuyaClientBuilderException("The client secret is empty or is missing. Please specify the client secret before building the client.");
            }

            return new TuyaClient(dataCenter, credentials, maxAuthRetryCount, logger);
        }
    }
}
