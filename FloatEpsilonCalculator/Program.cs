using FloatEpsilonCalculator.Extensions;
using FloatEpsilonCalculator.Tests;

Console.WriteLine(1.0f / FloatMathExtensions.FastInvSqrt(4.0f));
Console.WriteLine(1.0f / FloatMathExtensions.FastInvSqrt(100.0f));
Console.WriteLine(1.0f / FloatMathExtensions.FastInvSqrt(0.25f));

TestHelper.TestFloat("FLOAT MIN", BitConverter.Int32BitsToSingle(1 << (FloatMathExtensions.FLOAT_FRACTION_SIZE - 1)));
TestHelper.TestFloat("FLOAT SMALL", BitConverter.Int32BitsToSingle(1 << FloatMathExtensions.FLOAT_FRACTION_SIZE));
TestHelper.TestFloat("FLOAT BIGGER", BitConverter.Int32BitsToSingle(1 << (FloatMathExtensions.FLOAT_FRACTION_SIZE + 1)));
TestHelper.TestFloat("FLOAT UPPER", BitConverter.Int32BitsToSingle(FloatMathExtensions.FLOAT_FRACTION_SIZE << FloatMathExtensions.FLOAT_FRACTION_SIZE));
TestHelper.TestFloat("FLOAT NORMAL", BitConverter.Int32BitsToSingle((FloatMathExtensions.FLOAT_FRACTION_SIZE + 1) << FloatMathExtensions.FLOAT_FRACTION_SIZE));
TestHelper.TestFloat("512", 512.0f);

TestHelper.TestDouble("DOUBLE MIN", BitConverter.Int64BitsToDouble(1L << (DoubleMathExtensions.DOUBLE_FRACTION_SIZE - 1)));
TestHelper.TestDouble("DOUBLE SMALL", BitConverter.Int64BitsToDouble(1L << DoubleMathExtensions.DOUBLE_FRACTION_SIZE));
TestHelper.TestDouble("DOUBLE BIGGER", BitConverter.Int64BitsToDouble(1L << (DoubleMathExtensions.DOUBLE_FRACTION_SIZE + 1)));
TestHelper.TestDouble("DOUBLE UPPER", BitConverter.Int64BitsToDouble(((long)DoubleMathExtensions.DOUBLE_FRACTION_SIZE) << DoubleMathExtensions.DOUBLE_FRACTION_SIZE));
TestHelper.TestDouble("DOUBLE NORMAL", BitConverter.Int64BitsToDouble((DoubleMathExtensions.DOUBLE_FRACTION_SIZE + 1L) << DoubleMathExtensions.DOUBLE_FRACTION_SIZE));
TestHelper.TestDouble("512", 512.0);

Console.WriteLine("16384");
Console.WriteLine(16383.0f.FloatToBinary());
Console.WriteLine(16384.0f.FloatToBinary());
Console.WriteLine(16385.0f.FloatToBinary());

Console.WriteLine(16383.0.DoubleToBinary());
Console.WriteLine(16384.0.DoubleToBinary());
Console.WriteLine(16385.0.DoubleToBinary());
