using System.Text;

namespace CSharpReview.Decoder.Services
{
    public static class Helper
    {
        public static string ConvertByteToHex(byte[] value)
        {
            if(ReferenceEquals(value, null)) return string.Empty;
            if(value.Length < 1) return string.Empty;

            StringBuilder hex = new StringBuilder(value.Length * 2);
            foreach (byte item in value)
                hex.AppendFormat("{0:x2}", item);

            return hex.ToString();
        }
    }
}
