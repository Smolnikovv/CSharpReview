using CSharpReview.Decoder.Messages;

namespace CSharpReview.Decoder.Services
{
    public class Speed : MessageAbstract
    {
        const string MESSAGE_TYPE = "Speed";
        const string MESSAGE_HEX = "02";
        public override string Message { get; set; }

        private string _sequenceIdHex;
        private string _sequenceIdValue;
        private string _speedHex;
        private string _speedValue;
        private string _unitHex;
        private string _unitValue;
        private string _hex;

        private byte[] _data;

        public override void GenerateMessage(byte[] data)
        {
            if (data.Length < 4)
            {
                Message = "The array was too short";
                return;
            }

            _data = data;

            GetSequenceValues();
            GetSpeedValues();
            GetUnitValues();

            _hex = Helper.ConvertByteToHex(data);

            WriteMessage();
        }

        private void WriteMessage()
        {
            Message = $"{_hex}\n" +
                $"Field:\t\tHex\t\tValue \n" +
                $"Sequence ID:\t{_sequenceIdHex}\t\t{_sequenceIdValue}\n" +
                $"Message Type:\t{MESSAGE_HEX}\t\t{MESSAGE_TYPE}\n" +
                $"Speed:\t\t{_speedHex}\t\t{_speedValue}\n" +
                $"Unit:\t\t{_unitHex}\t\t{_unitValue}\n" +
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

        private void GetSpeedValues()
        {
            var speedArray = new byte[]
            {
                _data[3]
            };
            _speedHex = Helper.ConvertByteToHex(speedArray);
            _speedValue = Convert.ToInt32(_speedHex, 16).ToString();
        }

        private void GetUnitValues()
        {
            switch (_data[4])
            {
                case 0x00:
                    _unitValue = "MPH";
                    break;
                case 0x01:
                    _unitValue = "KPH";
                    break;
                default:
                    _unitValue = "unknown";
                    break;
            }
            _unitHex = Helper.ConvertByteToHex(new byte[] { _data[4] });
        }
    }
}
