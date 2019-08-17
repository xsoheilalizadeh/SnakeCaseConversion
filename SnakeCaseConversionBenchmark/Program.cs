using BenchmarkDotNet.Running;

namespace SnakeCaseConversionBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<SnakeCaseConventioneerBenchmark>();
        }
    }
}
