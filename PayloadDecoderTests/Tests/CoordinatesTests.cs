using CSharpReview.Decoder.Services;

namespace PayloadDecoderTests.Tests
{
    public class CoordinatesTests
    {
        [Test]
        public void GenerateMessage_WithValidData_ReturnsFormattedString()
        {
            var data = new byte[] { 0x00, 0x00, 0x01, 0x00, 0x3E, 0x32, 0xDF, 0x00, 0x70, 0xDC, 0x38, 0x0, 0x1 };
            var expected = $"000001003e32df0070dc380001\n" +
                $"Field:\t\tHex\t\tValue\n" +
                $"SequenceId:\t0000\t\t0\n" +
                $"MessageType:\t01\t\tCoordinates\n" +
                $"Latitude:\t003e32df\t40,76255\n" +
                $"Longitude:\t0070dc38\t73,96408\n" +
                $"LatitudeDir\t00\t\tNorth\n" +
                $"LongitudeDir\t01\t\tSouth\n" +
                $"----------------------------------------";

            var sut = new Coordinates();
            sut.GenerateMessage(data);
            var result = sut.Message;

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(new byte[] { }, "The array was too short")]
        public void GenerateMessage_WithInvalidData_ReturnsErrorMessage(byte[] values, string expected)
        {
            var sut = new Coordinates();
            sut.GenerateMessage(values);
            var result = sut.Message;

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
