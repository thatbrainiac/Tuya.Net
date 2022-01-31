using Tuya.Net.Data;
using Tuya.Net.Security;

namespace Tuya.Net.Api
{
    /// <summary>
    /// Tuya Client wrapper class.
    /// </summary>
    public class TuyaClient : ITuyaClient
    {
        /// <summary>
        /// An instance of <see cref="tuyaApiClient"/>.
        /// </summary>
        private readonly TuyaApiClient tuyaApiClient;

        /// <summary>
        /// Initializes a new instance of <see cref="TuyaClient"/> class.
        /// </summary>
        /// <param name="baseAddress">Base URI of the API.</param>
        /// <param name="credentials">Tuya API credentials.</param>
        public TuyaClient(string baseAddress, ITuyaCredentials credentials)
        {
            tuyaApiClient = new TuyaApiClient(baseAddress, credentials);
        }

        /// <inheritdoc />
        public async Task<AccessTokenInfo?> GetAccessTokenAsync()
        {
            return await tuyaApiClient.ReadAsync<AccessTokenInfo?>(HttpMethod.Get, "/v1.0/token?grant_type=1");
        }

        /// <inheritdoc />
        public async Task<TuyaDeviceInfo?> GetDeviceInfoAsync(string deviceId, AccessTokenInfo accessTokenInfo)
        {
            return await tuyaApiClient.ReadAsync<TuyaDeviceInfo?>(HttpMethod.Get, $"/v1.1/iot-03/devices/{deviceId}", accessTokenInfo);
        }

        /// <inheritdoc />
        public async Task<TuyaUser?> GetUserInfoAsync(string userId, AccessTokenInfo accessTokenInfo)
        {
            return await tuyaApiClient.ReadAsync<TuyaUser?>(HttpMethod.Get, $"/v1.0/users/{userId}/infos", accessTokenInfo);
        }
    }
}