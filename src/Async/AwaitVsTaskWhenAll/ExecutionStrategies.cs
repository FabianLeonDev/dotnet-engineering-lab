namespace DotnetEngineeringLab.Experiments.Async.AwaitVsTaskWhenAll;

public static class ExecutionStrategies
{
    public static async Task RunSequentiallyAsync(IReadOnlyList<Func<Task>> operations)
    {
        ArgumentNullException.ThrowIfNull(operations);

        foreach (var operation in operations)
        {
            ArgumentNullException.ThrowIfNull(operation);
            await operation().ConfigureAwait(false);
        }
    }

    public static Task RunConcurrentlyAsync(IReadOnlyList<Func<Task>> operations)
    {
        ArgumentNullException.ThrowIfNull(operations);

        var tasks = operations.Select(operation =>
        {
            ArgumentNullException.ThrowIfNull(operation);
            return operation();
        });

        return Task.WhenAll(tasks);
    }
}