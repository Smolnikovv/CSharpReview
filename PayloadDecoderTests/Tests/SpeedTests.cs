using CSharpReview.Decoder.Services;

namespace PayloadDecoderTests.Tests
{
    public class SpeedTests
    {
        [Test]
        public void GenerateMessage_WithValidData_ReturnsFormattedString()
        {
            var data = new byte[] { 0x00, 0x03, 0x02, 0x35, 0x01 };            
            var expected = $"0003023501\n" +
                $"Field:\t\tHex\t\tValue \n" +
                $"Sequence ID:\t0003\t\t3\n" +
                $"Message Type:\t02\t\tSpeed\n" +
                $"Speed:\t\t35\t\t53\n" +
                $"Unit:\t\t01\t\tKPH\n" +
                $"----------------------------------------";

            var sut = new Speed();
            sut.GenerateMessage(data);
            var result = sut.Message;

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(new byte[] { }, "The array was too short")]
        public void GenerateMessage_WithInvalidData_ReturnsErrorMessage(byte[] values, string expected)
        {
            var sut = new Speed();
            sut.GenerateMessage(values);
            var result = sut.Message;

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
