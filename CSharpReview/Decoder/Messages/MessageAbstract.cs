using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpReview.Decoder.Messages
{
    public abstract class MessageAbstract
    {
        public abstract string Message { get; set; }

        public abstract void GenerateMessage(byte[] data);
    }
}
