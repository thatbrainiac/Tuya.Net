using Tuya.Net.Data;

namespace Tuya.Net.Api
{
    /// <summary>
    /// Tuya Low-Level API Client Interface.
    /// </summary>
    public interface ITuyaLowLevelClient
    {
        /// <summary>
        /// Send request to Tuya API and read the response as a passed object type.
        /// </summary>
        /// <typeparam name="T">The object type to be deserialized to.</typeparam>
        /// <param name="method">The <see cref="HttpMethod"/> used.</param>
        /// <param name="path">Endpoint path.</param>
        /// <param name="accessToken">Tuya API access token.</param>
        /// <param name="payload">Request payload.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A deserialized <typeparamref name="T"/> object.</returns>
        public Task<T?> SendRequestAsync<T>(
            HttpMethod method,
            string path,
            IAccessToken? accessToken,
            string payload = "",
            CancellationToken cancellationToken = default);
    }
}
