using FloatEpsilonCalculator.Extensions;

namespace FloatEpsilonCalculator.Tests
{
    public static class TestHelper
    {
        public static void TestFloat(string name, float f)
        {
            var valueStr = f.FloatToBinary();

            Console.WriteLine(name);
            Console.WriteLine(valueStr);

            for (int i = 0; i < FloatMathExtensions.FLOAT_FRACTION_SIZE; i++)
            {
                var str = (f + f.GetFloatRelativeEpsilon(FloatMathExtensions.FLOAT_FRACTION_SIZE - i)).FloatToBinary();
                var charIndex = !f.IsNormalFloat() && i == FloatMathExtensions.FLOAT_FRACTION_SIZE - 1 ? FloatMathExtensions.FLOAT_FRACTION_SIZE + 1 : i;
                var status = str[str.Length - charIndex - 1] == '1' ? "OK" : "FAIL";
                Console.WriteLine($"{str} {status}");
            }
        }

        public static void TestDouble(string name, double d)
        {
            var valueStr = d.DoubleToBinary();

            Console.WriteLine(name);
            Console.WriteLine(valueStr);

            for (int i = 0; i < DoubleMathExtensions.DOUBLE_FRACTION_SIZE; i++)
            {
                var str = (d + d.GetDoubleRelativeEpsilon(DoubleMathExtensions.DOUBLE_FRACTION_SIZE - i)).DoubleToBinary();
                var charIndex = !d.IsNormalDouble() && i + 1 == DoubleMathExtensions.DOUBLE_FRACTION_SIZE ? DoubleMathExtensions.DOUBLE_FRACTION_SIZE + 1 : i;
                var status = str[str.Length - charIndex - 1] == '1' ? "OK" : "FAIL";
                Console.WriteLine($"{str} {status}");
            }
        }
    }
}
