using CSharpReview.Decoder;
using CSharpReview.Decoder.Messages;

namespace CSharpReview
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            PayloadDecoder decoder = new PayloadDecoder();
            List<MessageAbstract> messages = new List<MessageAbstract>();

            Payloads.PayloadExamples.ForEach(async payload =>
            {
                messages.Add(await decoder.DecodePayload(payload));
            });

            messages.ForEach(message =>
            {
                Console.WriteLine(message.Message);
            });
        }
    }
}
