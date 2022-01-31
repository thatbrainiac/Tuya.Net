﻿using Newtonsoft.Json;
using Tuya.Net.Data;
using Tuya.Net.Exceptions;
using Tuya.Net.Security;

namespace Tuya.Net.Api
{
    /// <summary>
    /// Tuya API client.
    /// </summary>
    internal class TuyaApiClient
    {
        /// <summary>
        /// HttpClient instance.
        /// </summary>
        private readonly HttpClient HttpClient;

        /// <summary>
        /// Tuya API client id.
        /// </summary>
        private readonly ITuyaCredentials Credentials;

        /// <summary>
        /// Creates a new instance of the <see cref="TuyaApiClient"/> class.
        /// </summary>
        /// <param name="baseAddress">Base address of the server.</param>
        /// <param name="credentials">Tuya API credentials.</param>
        public TuyaApiClient(string baseAddress, ITuyaCredentials credentials)
        {
            Credentials = credentials;
            HttpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress),
                DefaultRequestHeaders =
                {
                    { "client_id", credentials.ClientId },
                    { "sign_method", "HMAC-SHA256" }
                },
            };
        }

        /// <summary>
        /// Send request to Tuya API and read the response as a passed object type.
        /// </summary>
        /// <typeparam name="T">The object type to be deserialized to.</typeparam>
        /// <param name="method">The <see cref="HttpMethod"/> used.</param>
        /// <param name="path">Endpoint path.</param>
        /// <param name="payload">Request payload.</param>
        /// <returns>A deserialized <typeparamref name="T"/> object.</returns>
        internal async Task<T?> ReadAsync<T>(HttpMethod method, string path, AccessTokenInfo? accessTokenInfo = null, string payload = "")
        {
            var accessToken = accessTokenInfo == null ? string.Empty : accessTokenInfo.TokenString!;
            var now = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            var sign = EncryptionUtils.CalculateSignature(Credentials, method, path, payload, timestamp: now, accessToken, payload);

            var request = new HttpRequestMessage()
            {
                Method = method,
                RequestUri = new Uri(path, UriKind.Relative),
            };

            request.Headers.Add("t", now);
            request.Headers.Add("sign", sign);

            if (accessTokenInfo != null)
            {
                request.Headers.Add("access_token", accessToken);
            }

            var response = await HttpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Received a non-success status code from Tuya API. Code: {response.StatusCode}. Reason: {response.ReasonPhrase}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
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
                    throw new ArgumentNullException("Deserialized response was null.");
                }

                if (deserializedResponse.IsSuccess == null)
                {
                    throw new ArgumentNullException("Success status was null.");
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