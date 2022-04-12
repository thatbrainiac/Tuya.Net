using Microsoft.Extensions.Logging;
using Tuya.Net.Data;

namespace Tuya.Net.IoT
{
    /// <summary>
    /// User Manager class.
    /// </summary>
    public class UserManager : IUserManager
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
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        /// <param name="client">Tuya client instance.</param>
        /// <param name="logger">Logger instance.</param>
        public UserManager(ITuyaClient client, ILogger? logger)
        {
            this.logger = logger;
            this.client = client;
        }

        /// <inheritdoc />
        public async Task<User?> GetUserByIdAsync(string userId, IAccessToken? accessToken = default, CancellationToken ct = default)
        {
            logger?.LogInformation("Getting user: {userId}", userId);
            return await client.AuthenticatedRequestAsync<User?>(HttpMethod.Get, $"/v1.0/users/{userId}/infos", accessToken, cancellationToken: ct);
        }
    }
}
