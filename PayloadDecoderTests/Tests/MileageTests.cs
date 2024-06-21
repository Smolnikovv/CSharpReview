using CSharpReview.Decoder.Services;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;

namespace PayloadDecoderTests.Tests
{
    public class MileageTests
    {
        [Test]
        public void GenerateMessage_WithValidData_ReturnsFormattedString()
        {
            var data = new byte[] { 0x00, 0x05, 0x03, 0x00, 0x01, 0xE2, 0x40 };
            var expected = $"0005030001e240\n" +
                $"Field:\t\tHex\t\tValue \n" +
                $"Sequence ID:\t0005\t\t5\n" +
                $"Message Type:\t03\t\tMileage\n" +
                $"Mileage:\t0001e240\t123456\n" +
                $"----------------------------------------";

            var sut = new Mileage();
            sut.GenerateMessage(data);
            var result = sut.Message;

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(new byte[] { }, "The array was too short")]
        public void GenerateMessage_WithInvalidData_ReturnsErrorMessage(byte[] values, string expected)
        {
            var sut = new Mileage();
            sut.GenerateMessage(values);
            var result = sut.Message;

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
