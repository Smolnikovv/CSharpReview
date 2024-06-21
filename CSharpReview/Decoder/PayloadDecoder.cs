using CSharpReview.Decoder.Messages;
using CSharpReview.Decoder.Services;

namespace CSharpReview.Decoder
{
    public class PayloadDecoder
    {
        /// <summary>
        /// Input parameter and returned value should stay as is
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MessageAbstract> DecodePayload(byte[] payload)
        {
            if (payload == null) throw new Exception("Message can't be null");
            if (payload.Length < 3) throw new Exception("Message is too short");

            MessageAbstract decoder = null;
            var messageType = payload[2];
            switch(messageType)
            {
                case 0x01:
                    decoder = new Coordinates();
                    break;
                case 0x02:
                    decoder = new Speed();
                    break;
                case 0x03:
                    decoder = new Mileage();
                    break;
                default:
                    throw new Exception("Message type not found");
            }

            decoder.GenerateMessage(payload);
            
            return decoder;
        }
    }
}
