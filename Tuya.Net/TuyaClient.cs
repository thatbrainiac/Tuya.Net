using Tuya.Net.Api;
using Tuya.Net.Data;
using Tuya.Net.IoT;
using Tuya.Net.Security;

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
        /// <param name="baseAddress">Base URI of the API.</param>
        /// <param name="credentials">Tuya API credentials.</param>
        public TuyaClient(string baseAddress, ITuyaCredentials credentials)
        {
            LowLevel = new TuyaApiClient(baseAddress, credentials);
            DeviceManager = new DeviceManager(this);
            UserManager = new UserManager(this);
        }

        /// <summary>
        /// Access token.
        /// </summary>
        private IAccessToken? tuyaAccessToken;

        /// <summary>
        /// Add authentication to the Tuya client.
        /// </summary>
        public async Task<ITuyaClient> WithAuthentication(CancellationToken ct = default)
        {
            tuyaAccessToken = await GetAccessTokenInfoAsync(ct);
            return this;
        }

        /// <inheritdoc />
        public async Task<AccessTokenInfo?> GetAccessTokenInfoAsync(CancellationToken ct = default)
        {
            return await LowLevel.SendRequestAsync<AccessTokenInfo?>(HttpMethod.Get, "/v1.0/token?grant_type=1", null, cancellationToken: ct);
        }

        /// <inheritdoc />
        public async Task<T?> AuthenticatedRequestAsync<T>(HttpMethod httpMethod, string path, IAccessToken? accessToken, string payload = "", CancellationToken cancellationToken = default)
        {
            if (accessToken == null && tuyaAccessToken == null)
            {
                throw new ArgumentNullException(nameof(accessToken), "Missing access token for a request that requires authentication. Please provide it or set the access token in the client.");
            }
            return await LowLevel.SendRequestAsync<T>(httpMethod, path, accessToken ?? tuyaAccessToken!, payload, cancellationToken);
        }
    }
}