using System.Text;

namespace FloatEpsilonCalculator.Extensions
{
    public static class FloatMathExtensions
    {
        public const int FLOAT_FRACTION_SIZE = 23;

        public static string FloatToBinary(this float value)
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

        public static bool IsNormalFloat(this float f)
        {
            unsafe
            {
                var fAbs = (*(uint*)&f) & int.MaxValue; // (int)Math.Abs(value)
                return (fAbs >> FLOAT_FRACTION_SIZE) > 0;
            }
        }

        public static float GetFloatRelativeEpsilon(this float f, int precision = FLOAT_FRACTION_SIZE)
        {
#if DEBUG
            if (precision < 0)
                throw new ArgumentException("Precision can't be negative");

            if (precision > FLOAT_FRACTION_SIZE)
                throw new ArgumentException("Precision can't be bigger than Fraction Size");
#endif

            unsafe
            {
                var fAbs = (*(int*)&f) & int.MaxValue; // (int)Math.Abs(value)
                var errorReduction = FLOAT_FRACTION_SIZE - precision;
                var initialExponent = fAbs >> FLOAT_FRACTION_SIZE;

                // calculating epsilon in case when it is normal (exponent part > 0)
                var normalExp = initialExponent + errorReduction - FLOAT_FRACTION_SIZE;
                var normalEps = normalExp << FLOAT_FRACTION_SIZE;

                // calculating epsilon in case when it is subnormal (exponent part == 0)
                // when initialExponent is 0 or 1 no additional offset is needed
                var subnormalExp = initialExponent - 1;
                var subnormalEpsMultiplier = subnormalExp.IsPositiveInt();
                var subnormalEpsOffset = subnormalExp * subnormalEpsMultiplier + errorReduction;
                var subnormalEps = 1 << subnormalEpsOffset;

                // if epsilon is normal normalEps is returned, otherwise subnormalEps is returned
                var normalExpMultiplier = (normalExp - 1).IsPositiveInt();
                var subnormalExpMultiplier = normalExpMultiplier ^ 1;
                var eps = (normalEps * normalExpMultiplier) | (subnormalEps * subnormalExpMultiplier);

                return *(float*)&eps;
            }
        }

        public static float FastInvSqrt(float number)
        {
            var f = number;
            unsafe
            {
                var i = (uint*)&f;
                *i = 0x5f3759dfu - (*i >> 1);
                f *= 1.5f - (number * 0.5f * f * f);
                return f;
            }
        }
    }
}
