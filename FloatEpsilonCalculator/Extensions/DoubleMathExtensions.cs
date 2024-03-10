using System.Text;

namespace FloatEpsilonCalculator.Extensions
{
    public static class DoubleMathExtensions
    {
        public const int DOUBLE_EQUALITY_PRECISION = 40;
        public const int DOUBLE_FRACTION_SIZE = 52;

        public static string DoubleToBinary(this double d)
        {
            StringBuilder sb = new StringBuilder();
            var ba = BitConverter.GetBytes(d);
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

        public static bool IsNormalDouble(this double d)
        {
            unsafe
            {
                var dAbs = (*(ulong*)&d) & long.MaxValue; // (int)Math.Abs(value)
                return (dAbs >> DOUBLE_FRACTION_SIZE) > 0;
            }
        }

        public static double GetDoubleRelativeEpsilon(this double d, int precision = DOUBLE_FRACTION_SIZE)
        {
#if DEBUG
            if (precision < 0)
                throw new ArgumentException("Precision can't be negative");

            if (precision > DOUBLE_FRACTION_SIZE)
                throw new ArgumentException("Precision can't be bigger than Fraction Size");
#endif

            var value = Math.Abs(d);
            var errorReduction = DOUBLE_FRACTION_SIZE - precision;

            unsafe
            {
                var initialExponent = (*(long*)&value) >> DOUBLE_FRACTION_SIZE;
                var exponent = initialExponent + errorReduction;

                long eps;
                if (exponent <= DOUBLE_FRACTION_SIZE)
                {
                    eps = 1L;
                    eps <<= (int)Math.Max(initialExponent - 1, 0) + errorReduction;
                }
                else
                {
                    exponent -= DOUBLE_FRACTION_SIZE;
                    eps = exponent << DOUBLE_FRACTION_SIZE;
                }
                return *(double*)&eps;
            }
        }

        public static bool Equal(this double first, double second, int precision = DOUBLE_EQUALITY_PRECISION)
        {
            return Math.Abs(first - second) <= Math.Max(Math.Abs(first), Math.Abs(second)).GetDoubleRelativeEpsilon(precision);
        }

        public static bool AbsEqual(this double first, double second, double eps)
        {
            return Math.Abs(first - second) <= eps;
        }

        public static bool NotSmaller(this double first, double second, int precision = DOUBLE_EQUALITY_PRECISION)
        {
            return first > second || Equal(first, second, precision);
        }

        public static bool NotGreater(this double first, double second, int precision = DOUBLE_EQUALITY_PRECISION)
        {
            return first < second || Equal(first, second, precision);
        }

        public static bool Smaller(this double first, double second, int precision = DOUBLE_EQUALITY_PRECISION)
        {
            return first < second && !Equal(first, second, precision);
        }

        public static bool Greater(this double first, double second, int precision = DOUBLE_EQUALITY_PRECISION)
        {
            return first > second && !Equal(first, second, precision);
        }
    }
}
