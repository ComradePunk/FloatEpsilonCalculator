using System.Text;

namespace FloatEpsilonCalculator.Extensions
{
    public static class IntMathExtensions
    {
        public static string IntToBinary(this int value)
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
            string r = $"{s.Substring(0, 1)} {s.Substring(1, 8)} {s.Substring(9)}"; //sign exponent mantissa
            return r;
        }

        public static int IsPositiveInt(this int value)
        {
            unsafe
            {
                var uValue = *(uint*)&value;
                uValue ^= uint.MaxValue;
                uValue = uValue >> 31;
                return *(int*)&uValue;
            }
        }
    }
}
