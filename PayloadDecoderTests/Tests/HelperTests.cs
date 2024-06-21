using CSharpReview.Decoder.Services;

namespace PayloadDecoderTests.Tests
{
    public class HelperTests
    {
        [Test]
        [TestCase(new byte[] { 0x00, 0x01 }, "0001")]
        [TestCase(new byte[] { 0xA6, 0xB1 }, "A6B1")]
        [TestCase(new byte[] { 0x00, 0x01, 0x90, 0x20, 0x05 }, "0001902005")]
        [TestCase(new byte[] { 0x00 }, "00")]
        [TestCase(new byte[] { 0xFF }, "FF")]
        public void ConvertByteToHex_WithValidData_ReturnsHexString(byte[] values, string expected)
        {
            var result = Helper.ConvertByteToHex(values);

            Assert.That(result.ToUpper(), Is.EqualTo(expected));
        }

        [Test]
        [TestCase(new byte[] { }, "")]
        [TestCase(null, "")]
        public void ConvertByteToHex_WithEmptyOrNullData_ReturnsEmptyString(byte[] values, string expected)
        {
            var result = Helper.ConvertByteToHex(values);

            Assert.That(result.ToUpper(), Is.EqualTo(expected));
        }
    }
}
