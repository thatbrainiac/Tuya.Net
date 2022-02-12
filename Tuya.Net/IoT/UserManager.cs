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
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        /// <param name="client"></param>
        public UserManager(ITuyaClient client)
        {
            this.client = client;
        }

        /// <inheritdoc />
        public async Task<User?> GetUserByIdAsync(string userId, IAccessToken? accessToken = default, CancellationToken ct = default)
        {
            return await client.AuthenticatedRequestAsync<User?>(HttpMethod.Get, $"/v1.0/users/{userId}/infos", accessToken, cancellationToken: ct);
        }
    }
}
