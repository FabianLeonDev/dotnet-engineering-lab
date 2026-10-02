using DotnetEngineeringLab.Experiments.Async.AwaitVsTaskWhenAll;
using Xunit;

namespace DotnetEngineeringLab.Tests.Async.AwaitVsTaskWhenAll;

public sealed class ExecutionStrategiesTests
{
    [Fact]
    public async Task RunSequentiallyAsync_DoesNotOverlapOperations()
    {
        var (operations, getMaximumConcurrency) = CreateTrackedOperations(3);

        await ExecutionStrategies.RunSequentiallyAsync(operations);

        Assert.Equal(1, getMaximumConcurrency());
    }

    [Fact]
    public async Task RunConcurrentlyAsync_OverlapsIndependentOperations()
    {
        var (operations, getMaximumConcurrency) = CreateTrackedOperations(3);

        await ExecutionStrategies.RunConcurrentlyAsync(operations);

        Assert.Equal(3, getMaximumConcurrency());
    }

    private static (Func<Task>[] Operations, Func<int> GetMaximumConcurrency) CreateTrackedOperations(int count)
    {
        var activeOperations = 0;
        var maximumConcurrency = 0;
        var operations = Enumerable.Range(0, count)
            .Select(_ => (Func<Task>)(async () =>
            {
                var active = Interlocked.Increment(ref activeOperations);
                UpdateMaximum(ref maximumConcurrency, active);
                await Task.Delay(30);
                Interlocked.Decrement(ref activeOperations);
            }))
            .ToArray();

        return (operations, () => Volatile.Read(ref maximumConcurrency));
    }

    private static void UpdateMaximum(ref int maximum, int candidate)
    {
        var current = Volatile.Read(ref maximum);
        while (candidate > current)
        {
            var previous = Interlocked.CompareExchange(ref maximum, candidate, current);
            if (previous == current)
            {
                return;
            }

            current = previous;
        }
    }
}