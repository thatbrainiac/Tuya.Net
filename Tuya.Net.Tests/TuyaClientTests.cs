using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Tuya.Net.Api;
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
        private TuyaClient client = null!;

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
            accessTokenInfo = await client.GetAccessTokenAsync();
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
                var token = await client.GetAccessTokenAsync();
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
                var deviceInfo = await client.GetDeviceInfoAsync(config["TestDeviceId"], accessTokenInfo!);
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
                var userInfo = await client.GetUserInfoAsync(config["MyUserId"], accessTokenInfo!);
                Assert.IsNotNull(userInfo);
            });
        }
    }
}