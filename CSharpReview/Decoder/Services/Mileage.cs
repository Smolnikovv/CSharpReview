using CSharpReview.Decoder.Messages;

namespace CSharpReview.Decoder.Services
{
    public class Mileage : MessageAbstract
    {
        const string MESSAGE_TYPE = "Mileage";
        const string MESSAGE_HEX = "03";
        public override string Message { get; set; }

        private string _sequenceIdHex;
        private string _sequenceIdValue;
        private string _mileageHex;
        private string _mileageValue;
        private string _hex;

        private byte[] _data;

        public override void GenerateMessage(byte[] data)
        {
            if (data.Length < 7)
            {
                Message = "The array was too short";
                return;
            }
            _data = data;

            GetSequenceValues();
            GetMileageValues();

            _hex = Helper.ConvertByteToHex(data);

            WriteMessage();
        }

        private void WriteMessage()
        {
            Message = $"{_hex}\n" +
                $"Field:\t\tHex\t\tValue \n" +
                $"Sequence ID:\t{_sequenceIdHex}\t\t{_sequenceIdValue}\n" +
                $"Message Type:\t{MESSAGE_HEX}\t\t{MESSAGE_TYPE}\n" +
                $"Mileage:\t{_mileageHex}\t{_mileageValue}\n" +
                $"----------------------------------------";
        }

        private void GetSequenceValues()
        {
            var sequenceArray = new byte[]
            {
                _data[0],
                _data[1]
            };
            _sequenceIdHex = Helper.ConvertByteToHex(sequenceArray);
            _sequenceIdValue = Convert.ToInt32(_sequenceIdHex, 16).ToString();
        }

        private void GetMileageValues()
        {
            var mileageArray = new byte[]
            {
                _data[3],
                _data[4],
                _data[5],
                _data[6]
            };
            _mileageHex = Helper.ConvertByteToHex(mileageArray);
            _mileageValue = Convert.ToInt32(_mileageHex, 16).ToString();
        }
    }
}
