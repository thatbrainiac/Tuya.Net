using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Linq;
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
                .AddJsonFile("Config.json", false, true)
                .AddUserSecrets<TuyaClientTests>()
                .Build();

            var tuyaCreds = new TuyaCredentials()
            {
                ClientId = config["TuyaClientId"] ?? throw new ArgumentException("Tuya Client Id not configured! Add \"TuyaClientId\" to your secrets file."),
                ClientSecret = config["TuyaClientSecret"] ?? throw new ArgumentException("Tuya Client Secret not configured! Add \"TuyaSecret\" to your secrets file.")
            };

            client = await new TuyaClient(config["TuyaApiUrl"], tuyaCreds)
                .WithAuthentication();
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
                var deviceInfo = await client.GetDeviceInfoAsync(testDeviceId);
                Assert.IsNotNull(deviceInfo);
                Assert.AreEqual(testDeviceId, deviceInfo!.Id);
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
                var userInfo = await client.GetUserAsync(userId);
                Assert.IsNotNull(userInfo);
                Assert.AreEqual(userId, userInfo!.Id);
            });
        }

        /// <summary>
        /// Test obtaining a device.
        /// </summary>
        [Test]
        public void Test_GetDeviceAsync_DeviceObject()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                var testDeviceId = config["TestDeviceId"];
                AssertInconclusiveIfNullOrEmpty(testDeviceId);
                var device = await client.GetDeviceAsync(testDeviceId);
                Assert.IsNotNull(device);
                Assert.AreEqual(testDeviceId, device!.Id);
            });
        }

        /// <summary>
        /// Test sending a command to a device.
        /// </summary>
        [Test]
        public void Test_ToggleLightingDevice_HappyPath()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                var testDeviceId = config["TestDeviceId"];
                AssertInconclusiveIfNullOrEmpty(testDeviceId);
                var device = await client.GetDeviceAsync(testDeviceId);
                Assert.IsNotNull(device);

                var deviceStatusList = device!.StatusList;
                Assert.IsNotNull(deviceStatusList);

                var status = deviceStatusList!.FirstOrDefault(ds => ds.Code == "switch_led");
                Assert.IsNotNull(status);

                if (status!.Value is not bool)
                {
                    Assert.Fail("Cannot run test, the switch_led status did not return bool as expected.");
                }

                var isTurnedOn = (bool)status!.Value!;

                var command = new Command()
                {
                    Code = "switch_led",
                    Value = !isTurnedOn,
                };

                var result = await client.SendCommandAsync(device!, command);
                Assert.IsTrue(result);
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