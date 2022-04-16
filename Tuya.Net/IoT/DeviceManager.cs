using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Tuya.Net.Data;

namespace Tuya.Net.IoT
{
    internal class DeviceManager : IDeviceManager
    {
        /// <summary>
        /// Tuya client instance.
        /// </summary>
        private readonly ITuyaClient client;

        /// <summary>
        /// Logger instance.
        /// </summary>
        private readonly ILogger? logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceManager"/> class.
        /// </summary>
        /// <param name="client">Tuya client instance.</param>
        /// <param name="logger">Logger instance.</param>
        public DeviceManager(ITuyaClient client, ILogger? logger)
        {
            this.logger = logger;
            this.client = client;
        }

        /// <inheritdoc />
        public async Task<Device?> GetDeviceAsync(string deviceId, CancellationToken ct = default)
        {
            logger?.LogInformation("Getting device: {deviceId}", deviceId);
            return await client.RequestAsync<Device?>(HttpMethod.Get, $"/v1.0/devices/{deviceId}", cancellationToken: ct);
        }

        /// <inheritdoc />
        public async Task<DeviceInfo?> GetDeviceInfoAsync(string deviceId, CancellationToken ct = default)
        {
            logger?.LogInformation("Getting device information: {deviceId}", deviceId);
            return await client.RequestAsync<DeviceInfo?>(HttpMethod.Get, $"/v1.1/iot-03/devices/{deviceId}", cancellationToken: ct);
        }

        /// <inheritdoc />
        public async Task<IList<DeviceStatus>?> GetDeviceStatusAsync(DeviceInfo device, CancellationToken ct = default)
        {
            logger?.LogInformation("Getting device status for device: {deviceId}", device.Id ?? "unknown");
            ThrowIfInvalid(device);
            return await GetDeviceStatusAsync(device.Id!, ct);
        }

        /// <inheritdoc />
        public async Task<IList<DeviceStatus>?> GetDeviceStatusAsync(string deviceId, CancellationToken ct = default)
        {
            logger?.LogInformation("Getting device status for device: {deviceId}", deviceId);
            return await client.RequestAsync<IList<DeviceStatus>?>(HttpMethod.Get, $"/v1.0/devices/{deviceId}/status", cancellationToken: ct);
        }

        /// <inheritdoc />
        public async Task<IList<Device>?> GetDevicesByUserAsync(User user, CancellationToken cancellationToken = default)
        {
            logger?.LogInformation("Getting device list for user: {userId}", user.Id ?? "unknown");
            ThrowIfInvalid(user);
            return await GetDevicesByUserAsync(user.Id!, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IList<Device>?> GetDevicesByUserAsync(string userId, CancellationToken ct = default)
        {
            logger?.LogInformation("Getting device list for user: {userId}", userId);
            return await client.RequestAsync<IList<Device>?>(HttpMethod.Get, $"/v1.0/users/{userId}/devices", cancellationToken: ct);
        }

        /// <inheritdoc />
        public async Task<InstructionInfo?> GetDeviceInstructionsAsync(string deviceId, CancellationToken ct = default)
        {
            logger?.LogInformation("Getting device instructions for device: {deviceId}", deviceId);
            return await client.RequestAsync<InstructionInfo?>(HttpMethod.Get, $"/v1.0/devices/{deviceId}/functions", cancellationToken: ct);
        }

        /// <inheritdoc />
        public async Task<InstructionInfo?> GetDeviceInstructionsAsync(DeviceInfo device, CancellationToken ct = default)
        {
            logger?.LogInformation("Getting device instructions for device: {deviceId}", device.Id ?? "unknown");
            ThrowIfInvalid(device);
            return await GetDeviceInstructionsAsync(device.Id!, ct);
        }

        /// <inheritdoc />
        public async Task<bool> SendCommandAsync(DeviceInfo device, Command command, CancellationToken ct = default)
        {
            ThrowIfInvalid(device);
            return await SendCommandAsync(device.Id!, command, ct);
        }

        /// <inheritdoc />
        public async Task<bool> SendCommandAsync(string deviceId, Command command, CancellationToken ct = default)
        {
            var commands = new List<Command>() { command };
            return await SendCommandListAsync(deviceId, commands, ct);
        }

        /// <inheritdoc />
        public async Task<bool> SendCommandListAsync(DeviceInfo device, IList<Command> commands, CancellationToken ct = default)
        {
            ThrowIfInvalid(device);
            return await SendCommandListAsync(device.Id!, commands, ct);
        }

        /// <inheritdoc />
        public async Task<bool> SendCommandListAsync(string deviceId, IList<Command> commands, CancellationToken ct = default)
        {
            logger?.LogDebug("Sending command on {deviceId}: {commands}", deviceId, commands);
            return await client.RequestAsync<bool>(HttpMethod.Post, $"/v1.0/devices/{deviceId}/commands", JsonConvert.SerializeObject(new { commands }), cancellationToken: ct);
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
