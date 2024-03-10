using System.Text;

namespace FloatEpsilonCalculator.Extensions
{
    public static class LongMathExtensions
    {
        public static string LongToBinary(this long value)
        {
            StringBuilder sb = new StringBuilder();
            var ba = BitConverter.GetBytes(value);
            foreach (var b in ba)
            {
                for (int i = 0; i < 8; i++)
                {
                    sb.Insert(0, ((b >> i) & 1) == 1 ? "1" : "0");
                }
            }

            string s = sb.ToString();
            string r = $"{s.Substring(0, 1)} {s.Substring(1, 11)} {s.Substring(12)}"; //sign exponent mantissa
            return r;
        }
    }
}
