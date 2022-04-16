using Microsoft.Extensions.Logging;
using Tuya.Net.Api;
using Tuya.Net.Data;
using Tuya.Net.Data.Settings;
using Tuya.Net.Exceptions;
using Tuya.Net.IoT;

namespace Tuya.Net
{
    /// <summary>
    /// Tuya Client wrapper class.
    /// </summary>
    public class TuyaClient : ITuyaClient
    {
        /// <inheritdoc />
        public ITuyaLowLevelClient LowLevel { get; }

        /// <inheritdoc />
        public IDeviceManager DeviceManager { get; }

        /// <inheritdoc />
        public IUserManager UserManager { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="TuyaClient"/> class.
        /// </summary>
        /// <param name="dataCenter">Tuya data center.</param>
        /// <param name="credentials">Tuya API credentials.</param>
        /// <param name="maxAuthRetryCount">Max retry count for authorization attempts.</param>
        /// <param name="logger">Logger instance.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when an invalid data center has been provided.</exception>
        internal TuyaClient(DataCenter dataCenter, ITuyaCredentials credentials, int maxAuthRetryCount, ILogger<ITuyaClient>? logger = null)
            : this(GetBaseUri(dataCenter), credentials, logger)
        {
            this.maxAuthRetryCount = maxAuthRetryCount;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="TuyaClient"/> class.
        /// </summary>
        /// <param name="baseUrl">Tuya API base url.</param>
        /// <param name="credentials">Tuya API credentials.</param>
        /// <param name="logger">Logger instance.</param>
        private TuyaClient(string baseUrl, ITuyaCredentials credentials, ILogger<ITuyaClient>? logger = null)
        {
            this.logger = logger;
            LowLevel = new TuyaApiClient(baseUrl, credentials, logger);
            DeviceManager = new DeviceManager(this, logger);
            UserManager = new UserManager(this, logger);
        }

        /// <summary>
        /// <see cref="TuyaClient"/> logger.
        /// </summary>
        private readonly ILogger<ITuyaClient>? logger;

        /// <summary>
        /// Access token.
        /// </summary>
        private IAccessToken? tuyaAccessToken;

        /// <summary>
        /// Max retry count for authentication.
        /// </summary>
        private readonly int maxAuthRetryCount;

        /// <summary>
        /// Get the Tuya client builder.
        /// </summary>
        /// <returns></returns>
        public static ITuyaClientBuilder GetBuilder()
        {
            return new TuyaClientBuilder();
        }

        /// <inheritdoc />
        public async Task<T?> RequestAsync<T>(HttpMethod httpMethod, string path, string payload = "", CancellationToken cancellationToken = default)
        {
            logger?.LogInformation("Performing authenticated request: {httpMethod} {path}", httpMethod, path);

            if (tuyaAccessToken == null)
            {
                logger?.LogInformation("Access token is missing. Obtaining access token for ");
                tuyaAccessToken = await GetAccessTokenInfoAsync(ct: cancellationToken);
            }

            return await LowLevel.SendRequestAsync<T>(httpMethod, path, tuyaAccessToken, payload, cancellationToken);
        }

        /// <summary>
        /// Obtain the API access token.
        /// </summary>
        /// <returns>An <see cref="AccessTokenInfo"/> instance containing information about the access token.</returns>
        /// <exception cref="TuyaAuthenticationException">Thrown when the authentication failed after exceeding the max retry count.</exception>
        private async Task<AccessTokenInfo?> GetAccessTokenInfoAsync(int retryCount = 0, Exception? exception = null, CancellationToken ct = default)
        {
            logger?.LogInformation("Obtaining access token information from Tuya.");

            if (retryCount == maxAuthRetryCount)
            {
                logger?.LogError("Failed to authenticate to the Tuya server after {retryCount} retries. Please verify if your credentials are correct. Full exception: {exception}: {exception!.Message}", retryCount, exception, exception!.Message);
                throw new TuyaAuthenticationException($"Failed to authenticate to Tuya after {retryCount} retries. Please verify if your credentials are correct. Full exception: {exception!}");
            }

            try
            {
                return await LowLevel.SendRequestAsync<AccessTokenInfo?>(HttpMethod.Get, "/v1.0/token?grant_type=1", null, cancellationToken: ct);
            }
            catch (Exception ex)
            {
                return await GetAccessTokenInfoAsync(++retryCount, ex, ct);
            }
        }

        /// <summary>
        /// Retrieves the base URI for a given Tuya data center.
        /// </summary>
        /// <param name="dataCenter">Tuya data center.</param>
        /// <returns>The base URI for the provided <see cref="DataCenter"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when an invalid data center has been passed.</exception>
        private static string GetBaseUri(DataCenter dataCenter)
        {
            return dataCenter switch
            {
                DataCenter.EastUs => "https://openapi-ueaz.tuyaus.com",
                DataCenter.WestUs => "https://openapi.tuyaus.com",
                DataCenter.CentralEurope => "https://openapi.tuyaeu.com",
                DataCenter.WesternEurope => "https://openapi-weaz.tuyaeu.com",
                DataCenter.India => "https://openapi.tuyain.com",
                DataCenter.China => "https://openapi.tuyacn.com",
                DataCenter.Unknown => string.Empty,
                _ => throw new ArgumentOutOfRangeException(nameof(dataCenter), dataCenter, "Invalid data center provided!")
            };
        }
    }
}