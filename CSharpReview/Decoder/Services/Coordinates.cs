using CSharpReview.Decoder.Messages;

namespace CSharpReview.Decoder.Services
{
    public class Coordinates : MessageAbstract
    {
        const string MESSAGE_TYPE = "Coordinates";
        const string MESSAGE_HEX = "01";
        public override string Message { get; set; }

        private string _sequenceIdHex;
        private string _sequenceIdValue;
        private string _latitudeHex;
        private string _latitudeValue;
        private string _longitudeHex;
        private string _longitudeValue;
        private string _latitudeDirHex;
        private string _latitudeDirValue;
        private string _longitudeDirHex;
        private string _longitudeDirValue;

        private string _hex;
        private byte[] _data;

        public override void GenerateMessage(byte[] data)
        {
            if (data.Length < 13)
            {
                Message = "The array was too short";
                return;
            }

            _data = data;

            GetSequenceValues();
            GetLatitudeValues();
            GetLongitudeValues();
            GetLatitudeDirValues();
            GetLongitudeDirValues();

            _hex = Helper.ConvertByteToHex(data);

            WriteMessage();
        }

        private void WriteMessage()
        {
            Message = $"{_hex}\n" +
                $"Field:\t\tHex\t\tValue\n" +
                $"SequenceId:\t{_sequenceIdHex}\t\t{_sequenceIdValue}\n" +
                $"MessageType:\t{MESSAGE_HEX}\t\t{MESSAGE_TYPE}\n" +
                $"Latitude:\t{_latitudeHex}\t{_latitudeValue}\n" +
                $"Longitude:\t{_longitudeHex}\t{_longitudeValue}\n" +
                $"LatitudeDir\t{_latitudeDirHex}\t\t{_latitudeDirValue}\n" +
                $"LongitudeDir\t{_longitudeDirHex}\t\t{_longitudeDirValue}\n" +
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

        private void GetLatitudeValues()
        {
            var latitudeArray = new byte[]
            {
                _data[3],
                _data[4],
                _data[5],
                _data[6]
            };
            _latitudeHex = Helper.ConvertByteToHex(latitudeArray);
            _latitudeValue = Convert.ToInt32(_latitudeHex, 16).ToString();
            _latitudeValue = _latitudeValue.Insert(_latitudeValue.Length - 5, ",");
        }

        private void GetLongitudeValues()
        {
            var longitudeArray = new byte[]
            {
                _data[7],
                _data[8],
                _data[9],
                _data[10]
            };
            _longitudeHex = Helper.ConvertByteToHex(longitudeArray);
            _longitudeValue = Convert.ToInt32(_longitudeHex, 16).ToString();
            _longitudeValue = _longitudeValue.Insert(_longitudeValue.Length - 5, ",");
        }

        private void GetLatitudeDirValues()
        {
            switch (_data[11])
            {
                case 0x00:
                    _latitudeDirValue = "North";
                    break;
                case 0x01:
                    _latitudeDirValue = "South";
                    break;
                default:
                    _latitudeDirValue = "unkown";
                    break;
            }
            _latitudeDirHex = Helper.ConvertByteToHex(new byte[] { _data[11] });
        }

        private void GetLongitudeDirValues()
        {
            switch (_data[12])
            {
                case 0x00:
                    _longitudeDirValue = "North";
                    break;
                case 0x01:
                    _longitudeDirValue = "South";
                    break;
                default:
                    _longitudeDirValue = "unkown";
                    break;
            }
            _longitudeDirHex = Helper.ConvertByteToHex(new byte[] { _data[12] });
        }        
    }
}
