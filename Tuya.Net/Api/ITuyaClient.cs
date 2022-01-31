using Tuya.Net.Data;

namespace Tuya.Net.Api
{
    /// <summary>
    /// Tuya API client interface.
    /// </summary>
    public interface ITuyaClient
    {
        /// <summary>
        /// Get API access token.
        /// </summary>
        /// <returns>An <see cref="AccessTokenInfo"/> instance containing information about the access token.</returns>
        public Task<AccessTokenInfo?> GetAccessTokenInfoAsync();

        /// <summary>
        /// Get Tuya device information.
        /// </summary>
        /// <param name="deviceId">The ID of the device</param>
        /// <param name="accessTokenInfo">An <see cref="AccessTokenInfo"/> instance containing the access token string.</param>
        /// <returns>A <see cref="TuyaDeviceInfo"/> instance containing device information.</returns>
        public Task<TuyaDeviceInfo?> GetDeviceInfoAsync(string deviceId, AccessTokenInfo accessTokenInfo);

        /// <summary>
        /// Get user information about a user.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="accessTokenInfo">An <see cref="AccessTokenInfo"/> instance containing the access token string.</param>
        /// <returns></returns>
        public Task<TuyaUser?> GetUserInfoAsync(string userId, AccessTokenInfo accessTokenInfo);
    }
}
