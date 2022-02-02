using NUnit.Framework;
using Tuya.Net.Security;

namespace Tuya.Net.Tests.Security
{
    /// <summary>
    /// EncryptionUtils Tests.
    /// </summary>
    [TestFixture]
    public class EncryptionUtilsTests
    {
        /// <summary>
        /// Test the validation of a SHA-256 string.
        /// </summary>
        [Test]
        public void Test_ToSha256_ValidSha()
        {
            const string input = "{\"commands\":[{\"code\": \"switch_led\",\"value\": false}]}";
            const string expectedSha = "dd4797de5d3d1e8f29b0ea46b6c597d043a1ca450f6d1e032583f8cdd1d9d91d";
            var actualSha = input.ToSha256();
            Assert.AreEqual(expectedSha, actualSha);
        }
    }
}
