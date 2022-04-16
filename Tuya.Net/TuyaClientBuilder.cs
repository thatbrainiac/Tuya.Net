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
        private ITuyaCredentials? credentials;
        
        /// <summary>
        /// Logger instance.
        /// </summary>
        private ILogger<ITuyaClient>? logger;

        /// <summary>
        /// Max auth retry count.
        /// </summary>
        private int maxAuthRetryCount = 3;

        /// <inheritdoc />
        public ITuyaClientBuilder UsingDataCenter(DataCenter dataCenter)
        {
            this.dataCenter = dataCenter;
            return this;
        }

        /// <inheritdoc />
        public ITuyaClientBuilder UsingCredentials(ITuyaCredentials credentials)
        {
            this.credentials = credentials;
            return this;
        }

        /// <inheritdoc />
        public ITuyaClientBuilder UsingLogger(ILogger<ITuyaClient> logger)
        {
            this.logger = logger;
            return this;
        }

        /// <inheritdoc />
        public ITuyaClientBuilder MaxAuthRetryCount(int maxAuthRetryCount)
        {
            this.maxAuthRetryCount = maxAuthRetryCount;
            return this;
        }

        /// <inheritdoc />
        public ITuyaClient Build()
        {
            if (dataCenter == DataCenter.Unknown)
            {
                throw new TuyaClientBuilderException("No data center has been provided. Please provide a data center first before building the client.");
            }

            if (credentials == null)
            {
                throw new TuyaClientBuilderException("No credentials have been provided. Please provide valid credentials first before building the client.");
            }

            return new TuyaClient(dataCenter, credentials, maxAuthRetryCount, logger);
        }
    }
}
