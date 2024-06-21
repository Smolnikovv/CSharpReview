using CSharpReview.Decoder;
using CSharpReview.Decoder.Services;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace PayloadDecoderTests.Tests
{
    public class PayloadDecoderTests
    {
        [Test]
        [TestCase(new byte[] { 0x00, 0x00, 0x01, 0x00, 0x3E, 0x32, 0xDF, 0x00, 0x70, 0xDC, 0x38, 0x0, 0x1 },
            typeof(Coordinates))]
        [TestCase(new byte[] { 0x00, 0x02, 0x02, 0x10, 0x00 },
            typeof(Speed))]
        [TestCase(new byte[] { 0x00, 0x05, 0x03, 0x00, 0x01, 0xE2, 0x40 },
            typeof(Mileage))]
        public async Task DecodePayload_WithValidData_ReturnsCorrectObjectType(byte[] data, Type expected)
        {
            var sut = new PayloadDecoder();
            var result = await sut.DecodePayload(data);

            Assert.IsInstanceOf(expected, result);
        }

        [Test]
        [TestCase(new byte[] { 0x00 }, "Message is too short")]
        [TestCase(null, "Message can't be null")]
        [TestCase(new byte[] { 0x00, 0x01, 0x06, 0x00, 0x02 }, "Message type not found")]
        public void DecodePayload_WithInvalidData_ThrowsException(byte[] data, string expected)
        {
            var sut = new PayloadDecoder();
            var result = Assert.ThrowsAsync<Exception>(async () => await sut.DecodePayload(data));

            Assert.AreEqual(expected, result.Message);
        }
    }
}
