using Newtonsoft.Json;
using Tuya.Net.Api;
using Tuya.Net.Data;
using Tuya.Net.Security;

namespace Tuya.Net
{
    /// <summary>
    /// Tuya Client wrapper class.
    /// </summary>
    public class TuyaClient : ITuyaClient
    {
        /// <summary>
        /// Gets the low-level <see cref="ITuyaLowLevelClient"/> instance for custom requests.
        /// </summary>
        public ITuyaLowLevelClient LowLevel { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="TuyaClient"/> class.
        /// </summary>
        /// <param name="baseAddress">Base URI of the API.</param>
        /// <param name="credentials">Tuya API credentials.</param>
        public TuyaClient(string baseAddress, ITuyaCredentials credentials)
        {
            LowLevel = new TuyaApiClient(baseAddress, credentials);
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
        public async Task<Device?> GetDeviceAsync(string deviceId, IAccessToken? accessToken = default, CancellationToken ct = default)
        {
            return await AuthenticatedRequestAsync<Device?>(HttpMethod.Get, $"/v1.0/devices/{deviceId}", accessToken, cancellationToken: ct);
        }

        /// <inheritdoc />
        public async Task<DeviceInfo?> GetDeviceInfoAsync(string deviceId, IAccessToken? accessToken = default, CancellationToken ct = default)
        {
            return await AuthenticatedRequestAsync<DeviceInfo?>(HttpMethod.Get, $"/v1.1/iot-03/devices/{deviceId}", accessToken, cancellationToken: ct);
        }

        /// <inheritdoc />
        public async Task<IList<DeviceStatus>?> GetDeviceStatusAsync(DeviceInfo device, IAccessToken? accessToken = default, CancellationToken ct = default)
        {
            ThrowIfInvalid(device);
            return await GetDeviceStatusAsync(device.Id!, accessToken, ct);
        }

        /// <inheritdoc />
        public async Task<IList<DeviceStatus>?> GetDeviceStatusAsync(string deviceId, IAccessToken? accessToken = default, CancellationToken ct = default)
        {
            return await AuthenticatedRequestAsync<IList<DeviceStatus>?>(HttpMethod.Get, $"/v1.0/devices/{deviceId}/status", accessToken, cancellationToken: ct);
        }

        /// <inheritdoc />
        public async Task<User?> GetUserAsync(string userId, IAccessToken? accessToken = default, CancellationToken ct = default)
        {
            return await AuthenticatedRequestAsync<User?>(HttpMethod.Get, $"/v1.0/users/{userId}/infos", accessToken, cancellationToken: ct);
        }

        /// <inheritdoc />
        public async Task<IList<Device>?> GetDevicesByUserAsync(User user, IAccessToken? accessToken = default, CancellationToken cancellationToken = default)
        {
            ThrowIfInvalid(user);
            return await GetDevicesByUserAsync(user.Id!, accessToken, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IList<Device>?> GetDevicesByUserAsync(string userId, IAccessToken? accessToken = default, CancellationToken ct = default)
        {
            return await AuthenticatedRequestAsync<IList<Device>?>(HttpMethod.Get, $"/v1.0/users/{userId}/devices", accessToken, cancellationToken: ct);
        }

        /// <inheritdoc />
        public async Task<InstructionInfo?> GetDeviceInstructionsAsync(string deviceId, IAccessToken? accessToken = default, CancellationToken ct = default)
        {
            return await AuthenticatedRequestAsync<InstructionInfo?>(HttpMethod.Get, $"/v1.0/devices/{deviceId}/functions", accessToken, cancellationToken: ct);
        }

        /// <inheritdoc />
        public async Task<InstructionInfo?> GetDeviceInstructionsAsync(DeviceInfo device, IAccessToken? accessToken = default, CancellationToken ct = default)
        {
            ThrowIfInvalid(device);
            return await GetDeviceInstructionsAsync(device.Id!, accessToken, ct);
        }

        /// <inheritdoc />
        public async Task<bool> SendCommandAsync(DeviceInfo device, Command command, IAccessToken? accessToken = default, CancellationToken ct = default)
        {
            ThrowIfInvalid(device);
            return await SendCommandAsync(device.Id!, command, accessToken, ct);
        }

        /// <inheritdoc />
        public async Task<bool> SendCommandAsync(string deviceId, Command command, IAccessToken? accessToken = default, CancellationToken ct = default)
        {
            var commands = new List<Command>() { command };
            return await SendCommandListAsync(deviceId, commands, accessToken, ct);
        }

        /// <inheritdoc />
        public async Task<bool> SendCommandListAsync(DeviceInfo device, IList<Command> commands, IAccessToken? accessToken = default, CancellationToken ct = default)
        {
            ThrowIfInvalid(device);
            return await SendCommandListAsync(device.Id!, commands, accessToken, ct);
        }

        /// <inheritdoc />
        public async Task<bool> SendCommandListAsync(string deviceId, IList<Command> commands, IAccessToken? accessToken = default, CancellationToken ct = default)
        {
            return await AuthenticatedRequestAsync<bool>(HttpMethod.Post, $"/v1.0/devices/{deviceId}/commands", accessToken, JsonConvert.SerializeObject(new { commands }), cancellationToken: ct);
        }

        /// <summary>
        /// Helper method to call low-level client requests.
        /// </summary>
        /// <typeparam name="T">Object type to return.</typeparam>
        /// <param name="httpMethod">HTTP method.</param>
        /// <param name="path">API path.</param>
        /// <param name="accessToken">Access token.</param>
        /// <param name="payload">Payload string, if present.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>An instance of <see cref="T"/> containing the requested data.</returns>
        private async Task<T?> AuthenticatedRequestAsync<T>(HttpMethod httpMethod, string path, IAccessToken? accessToken, string payload = "", CancellationToken cancellationToken = default)
        {
            if (accessToken == null && tuyaAccessToken == null)
            {
                throw new ArgumentNullException(nameof(accessToken), "Missing access token for a request that requires authentication. Please provide it or set the access token in the client.");
            }
            return await LowLevel.SendRequestAsync<T>(httpMethod, path, accessToken ?? tuyaAccessToken!, payload, cancellationToken);
        }

        /// <summary>
        /// Helper method to throw if a passed Tuya identifiable object is null or its ID is null.
        /// </summary>
        /// <param name="tuyaObject">The Tuya object.</param>
        /// <exception cref="ArgumentNullException">Thrown if the passed Tuya object is null, or its ID is null.</exception>
        private static void ThrowIfInvalid(IIdentifiable tuyaObject)
        {
            ArgumentNullException.ThrowIfNull(tuyaObject);
            ArgumentNullException.ThrowIfNull(tuyaObject.Id);
        }
    }
}