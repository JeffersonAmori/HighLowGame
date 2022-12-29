using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;
using Moq;

namespace LoggerAdapter.Benchmarks;

[MemoryDiagnoser]
public sealed class Benchmark
{
    [Params(100, 1000, 10000)]
    public int Count;

    [Benchmark]
    public void LoggerAdapter_WithValueType()
    {
        var mock = new Mock<ILoggerAdapter<Benchmark>>();
        var logger = mock.Object;

        for (var i = 0; i < Count; i++)
        {
            logger.LogInformation("Message: {message}", 2);
        }
    }

    [Benchmark]
    public void LoggerAdapter_WithReferenceType()
    {
        var mock = new Mock<ILoggerAdapter<Benchmark>>();
        var logger = mock.Object;

        for (var i = 0; i < Count; i++)
        {
            logger.LogInformation("Message: {message}", "reference type");
        }
    }

    [Benchmark(Baseline = true)]
    public void ILogger_WithValueType()
    {
        var mock = new Mock<ILogger<Benchmark>>();
        var logger = mock.Object;

        for (var i = 0; i < Count; i++)
        {
            logger.LogInformation("Message: {message}", 2);
        }
    }

    [Benchmark]
    public void ILogger_WithReferenceType()
    {
        var mock = new Mock<ILogger<Benchmark>>();
        var logger = mock.Object;

        for (var i = 0; i < Count; i++)
        {
            logger.LogInformation("Message: {message}", "reference type");
        }
    }
}