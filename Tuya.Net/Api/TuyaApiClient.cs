using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Tuya.Net.Data;
using Tuya.Net.Exceptions;
using Tuya.Net.Security;

namespace Tuya.Net.Api
{
    /// <summary>
    /// Tuya API client.
    /// </summary>
    public class TuyaApiClient : ITuyaLowLevelClient
    {
        /// <summary>
        /// HttpClient instance.
        /// </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// Tuya API client id.
        /// </summary>
        private readonly ITuyaCredentials credentials;

        /// <summary>
        /// Logger instance.
        /// </summary>
        private readonly ILogger? logger;

        /// <summary>
        /// Creates a new instance of the <see cref="TuyaApiClient"/> class.
        /// </summary>
        /// <param name="baseAddress">Base address of the server.</param>
        /// <param name="credentials">Tuya API credentials.</param>
        /// <param name="logger">Logger instance./</param>
        public TuyaApiClient(string baseAddress, ITuyaCredentials credentials, ILogger? logger)
        {
            this.logger = logger;
            this.credentials = credentials;
            httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress),
                DefaultRequestHeaders =
                {
                    { "client_id", credentials.ClientId },
                    { "sign_method", "HMAC-SHA256" }
                },
            };
        }

        /// <inheritdoc />
        public async Task<T?> SendRequestAsync<T>(HttpMethod method, string path, IAccessToken? accessToken, string payload = "", CancellationToken cancellationToken = default)
        {
            logger?.LogDebug("HTTP SEND: {method} {path} - Payload: {payload}", method, path, payload == string.Empty ? "<empty>" : payload);
            cancellationToken.ThrowIfCancellationRequested();

            var accessTokenValue = accessToken == null ? string.Empty : accessToken.Value!;
            var currentTimeMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            var sign = EncryptionUtils.CalculateSignature(credentials, method, path, payload, currentTimeMillis, accessTokenValue);

            var request = new HttpRequestMessage()
            {
                Method = method,
                RequestUri = new Uri(path, UriKind.Relative),
            };

            request.Headers.Add("t", currentTimeMillis);
            request.Headers.Add("sign", sign);

            if (accessToken != null)
            {
                request.Headers.Add("access_token", accessTokenValue);
            }

            if (payload != string.Empty)
            {
                request.Content = new StringContent(payload);
            }

            var response = await httpClient.SendAsync(request, cancellationToken);
            logger?.LogDebug("HTTP RECEIVE: {response}", response);

            if (!response.IsSuccessStatusCode)
            {
                throw new TuyaResponseException($"Received a non-success status code from Tuya API. Code: {response.StatusCode}. Reason: {response.ReasonPhrase}");
            }

            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            return DeserializeResponse<T>(responseContent);
        }

        /// <summary>
        /// Deserialize a response into an object.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="responseContent">Response content string.</param>
        /// <returns>An object of type <typeparamref name="T"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the deserialization result was null.</exception>
        /// <exception cref="TuyaResponseException">Thrown when deserialization failed or there was an error in Tuya's response.</exception>
        private static T? DeserializeResponse<T>(string responseContent)
        {
            try
            {
                var deserializedResponse = JsonConvert.DeserializeObject<TuyaResponse<T>>(responseContent);
                if (deserializedResponse == null)
                {
                    throw new ArgumentNullException(nameof(responseContent));
                }

                if (deserializedResponse.IsSuccess == null)
                {
                    throw new ArgumentNullException(nameof(responseContent));
                }

                if ((bool)!deserializedResponse.IsSuccess)
                {
                    throw new TuyaResponseException(deserializedResponse.ErrorCode!, deserializedResponse.ErrorMessage!);
                }

                return deserializedResponse.Result;
            }
            catch (JsonSerializationException)
            {
                throw new TuyaResponseException("An error occurred while attempting to deserialize the response from Tuya API.");
            }
        }
    }
}
