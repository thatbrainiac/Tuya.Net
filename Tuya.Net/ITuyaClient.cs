using Tuya.Net.Api;
using Tuya.Net.Data;

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
        /// Get API access token.
        /// </summary>
        /// <returns>An <see cref="AccessTokenInfo"/> instance containing information about the access token.</returns>
        public Task<AccessTokenInfo?> GetAccessTokenInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get Tuya device (information and status).
        /// </summary>
        /// <param name="deviceId">Tuya device ID.</param>
        /// <param name="accessToken">An <see cref="IAccessToken"/> instance containing the access token string.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A <see cref="Device"/> instance containing device information and status.</returns>
        public Task<Device?> GetDeviceAsync(string deviceId, IAccessToken? accessToken = default, CancellationToken ct = default);

        /// <summary>
        /// Get Tuya device information.
        /// </summary>
        /// <param name="deviceId">The ID of the device.</param>
        /// <param name="accessToken">An <see cref="IAccessToken"/> instance containing the access token string.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A <see cref="DeviceInfo"/> instance containing device information.</returns>
        public Task<DeviceInfo?> GetDeviceInfoAsync(string deviceId, IAccessToken? accessToken = default, CancellationToken ct = default);

        /// <summary>
        /// Get Tuya device status.
        /// </summary>
        /// <param name="device">Tuya device instance.</param>
        /// <param name="accessToken">An <see cref="IAccessToken"/> instance containing the access token string.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A <see cref="DeviceInfo"/> instance containing device status.</returns>
        public Task<IList<DeviceStatus>?> GetDeviceStatusAsync(DeviceInfo device, IAccessToken? accessToken = default, CancellationToken ct = default);

        /// <summary>
        /// Get Tuya device status.
        /// </summary>
        /// <param name="deviceId">The ID of the device.</param>
        /// <param name="accessToken">An <see cref="IAccessToken"/> instance containing the access token string.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A <see cref="DeviceInfo"/> instance containing device status.</returns>
        public Task<IList<DeviceStatus>?> GetDeviceStatusAsync(string deviceId, IAccessToken? accessToken = default, CancellationToken ct = default);

        /// <summary>
        /// Get user information.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="accessToken">An <see cref="IAccessToken"/> instance containing the access token string.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A <see cref="User"/> containing user information.</returns>
        public Task<User?> GetUserAsync(string userId, IAccessToken? accessToken = default, CancellationToken ct = default);

        /// <summary>
        /// Get a list of devices by user.
        /// </summary>
        /// <param name="user">Tuya user.</param>
        /// <param name="accessToken">An <see cref="IAccessToken"/> instance containing the access token string.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A list of <see cref="Device"/>s associated with the provided user.</returns>
        public Task<IList<Device>?> GetDevicesByUserAsync(User user, IAccessToken? accessToken = default, CancellationToken ct = default);

        /// <summary>
        /// Get a list of devices by user ID.
        /// </summary>
        /// <param name="userId">Tuya user ID.</param>
        /// <param name="accessToken">An <see cref="IAccessToken"/> instance containing the access token string.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A list of <see cref="Device"/>s associated with the provided user.</returns>
        public Task<IList<Device>?> GetDevicesByUserAsync(string userId, IAccessToken? accessToken = default, CancellationToken ct = default);

        /// <summary>
        /// Get a list of devices by device ID.
        /// </summary>
        /// <param name="deviceId">Tuya device ID.</param>
        /// <param name="accessToken">An <see cref="IAccessToken"/> instance containing the access token string.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A list of instructions for the given device.</returns>
        public Task<InstructionInfo?> GetDeviceInstructionsAsync(string deviceId, IAccessToken? accessToken = default, CancellationToken ct = default);

        /// <summary>
        /// Get a list of devices by device ID.
        /// </summary>
        /// <param name="device">Tuya device ID.</param>
        /// <param name="accessToken">An <see cref="IAccessToken"/> instance containing the access token string.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A list of instructions for the given device.</returns>
        public Task<InstructionInfo?> GetDeviceInstructionsAsync(DeviceInfo device, IAccessToken? accessToken = default, CancellationToken ct = default);

        /// <summary>
        /// Send a single command to a device.
        /// </summary>
        /// <param name="device">Tuya device.</param>
        /// <param name="command">The <see cref="Command"/> to send.</param>
        /// <param name="accessToken">An <see cref="IAccessToken"/> instance containing the access token string.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>True if the command was executed successfully, false otherwise.</returns>
        public Task<bool> SendCommandAsync(DeviceInfo device, Command command, IAccessToken? accessToken = default, CancellationToken ct = default);

        /// <summary>
        /// Send device commands.
        /// </summary>
        /// <param name="device">Tuya device.</param>
        /// <param name="commands">A list of <see cref="Command"/>s to send.</param>
        /// <param name="accessToken">An <see cref="IAccessToken"/> instance containing the access token string.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>True if the command set was executed successfully, false otherwise.</returns>
        public Task<bool> SendCommandListAsync(DeviceInfo device, IList<Command> commands,
            IAccessToken? accessToken = default, CancellationToken ct = default);

        /// <summary>
        /// Send a single command to a device.
        /// </summary>
        /// <param name="deviceId">Tuya device ID.</param>
        /// <param name="command">The <see cref="Command"/> to send.</param>
        /// <param name="accessToken">An <see cref="IAccessToken"/> instance containing the access token string.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>True if the command was executed successfully, false otherwise.</returns>
        public Task<bool> SendCommandAsync(string deviceId, Command command, IAccessToken? accessToken = default, CancellationToken ct = default);

        /// <summary>
        /// Send device commands.
        /// </summary>
        /// <param name="deviceId">Tuya device ID.</param>
        /// <param name="commands">A list of <see cref="Command"/>s to send.</param>
        /// <param name="accessToken">An <see cref="IAccessToken"/> instance containing the access token string.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>True if the command set was executed successfully, false otherwise.</returns>
        public Task<bool> SendCommandListAsync(string deviceId, IList<Command> commands, IAccessToken? accessToken = default, CancellationToken ct = default);
    }
}
