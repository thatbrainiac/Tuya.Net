using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Tuya.Net.Data;
using Tuya.Net.Security;

namespace Tuya.Net.Tests
{
    /// <summary>
    /// Tuya Client tests.
    /// </summary>
    [TestFixture]
    public class TuyaClientTests
    {
        /// <summary>
        /// Tuya client instance.
        /// </summary>
        private ITuyaClient client = null!;

        /// <summary>
        /// Access token info.
        /// </summary>
        private AccessTokenInfo? accessTokenInfo;

        /// <summary>
        /// Configuration instance.
        /// </summary>
        private IConfigurationRoot config = null!;

        /// <summary>
        /// One time setup.
        /// </summary>
        [OneTimeSetUp]
        public async Task SetUpAsync()
        {
            config = new ConfigurationBuilder()
                .AddJsonFile("Config.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<TuyaClientTests>()
                .Build();

            var tuyaCreds = new TuyaCredentials()
            {
                ClientId = config["TuyaClientId"] ?? throw new ArgumentException("Tuya Client Id not configured! Add \"TuyaClientId\" to your secrets file."),
                ClientSecret = config["TuyaClientSecret"] ?? throw new ArgumentException("Tuya Client Secret not configured! Add \"TuyaSecret\" to your secrets file.")
            };

            client = new TuyaClient(config["TuyaApiUrl"], tuyaCreds);
            accessTokenInfo = await client.GetAccessTokenInfoAsync();
            Assert.IsNotNull(accessTokenInfo);
        }

        /// <summary>
        /// Test obtaining the access token from Tuya.
        /// </summary>
        [Test]
        [Ignore("Covered by other integration tests.")]
        public void Test_GetAccessTokenInfo_AccessTokenObject()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                var token = await client.GetAccessTokenInfoAsync();
                Assert.IsNotNull(token);
            });
        }

        /// <summary>
        /// Test obtaining device information.
        /// </summary>
        [Test]
        public void Test_GetDeviceInfo_DeviceInfoObject()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                var testDeviceId = config["TestDeviceId"];
                AssertInconclusiveIfNullOrEmpty(testDeviceId);
                var deviceInfo = await client.GetDeviceInfoAsync(testDeviceId, accessTokenInfo!);
                Assert.IsNotNull(deviceInfo);
            });
        }

        /// <summary>
        /// Test obtaining user information.
        /// </summary>
        [Test]
        public void Test_GetUserInfoAsync_UserInfoObject()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                var userId = config["MyUserId"];
                AssertInconclusiveIfNullOrEmpty(userId);
                var userInfo = await client.GetUserInfoAsync(userId, accessTokenInfo!);
                Assert.IsNotNull(userInfo);
            });
        }

        /// <summary>
        /// Helper method to assert inconclusive if config item is null or empty.
        /// </summary>
        /// <param name="configItem">Configuration item.</param>
        private static void AssertInconclusiveIfNullOrEmpty(string configItem)
        {
            if (string.IsNullOrEmpty(configItem))
            {
                Assert.Inconclusive($"Config item is null or empty: {nameof(configItem)}");
            }
        }
    }
}