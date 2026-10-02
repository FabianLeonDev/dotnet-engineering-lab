using System.Diagnostics;
using DotnetEngineeringLab.Experiments.Async.AwaitVsTaskWhenAll;

var scenarios = new[]
{
	new Scenario("3 tasks x 80 ms", [80, 80, 80]),
	new Scenario("5 tasks x 80 ms", [80, 80, 80, 80, 80]),
	new Scenario("Mixed delays", [40, 80, 120])
};

const int sampleCount = 3;
Console.WriteLine("Independent asynchronous I/O simulation (median of 3 samples)");
Console.WriteLine("Times are illustrative; results depend on the environment.");
Console.WriteLine();
Console.WriteLine($"{ "Scenario",-20} { "Tasks",5} { "Sequential (ms)",18} { "Task.WhenAll (ms)",19} { "Time saved (ms)",17}");

foreach (var scenario in scenarios)
{
	var operations = scenario.DelaysMilliseconds
		.Select(delay => (Func<Task>)(() => Task.Delay(delay)))
		.ToArray();
	var sequentialSamples = new List<double>(sampleCount);
	var concurrentSamples = new List<double>(sampleCount);

	for (var sample = 0; sample < sampleCount; sample++)
	{
		if (sample % 2 == 0)
		{
			sequentialSamples.Add(await MeasureAsync(() => ExecutionStrategies.RunSequentiallyAsync(operations)));
			concurrentSamples.Add(await MeasureAsync(() => ExecutionStrategies.RunConcurrentlyAsync(operations)));
		}
		else
		{
			concurrentSamples.Add(await MeasureAsync(() => ExecutionStrategies.RunConcurrentlyAsync(operations)));
			sequentialSamples.Add(await MeasureAsync(() => ExecutionStrategies.RunSequentiallyAsync(operations)));
		}
	}

	var sequentialMedian = Median(sequentialSamples);
	var concurrentMedian = Median(concurrentSamples);
	Console.WriteLine($"{scenario.Name,-20} {operations.Length,5} {sequentialMedian,18:F1} {concurrentMedian,19:F1} {sequentialMedian - concurrentMedian,17:F1}");
}

static async Task<double> MeasureAsync(Func<Task> operation)
{
	var stopwatch = Stopwatch.StartNew();
	await operation();
	stopwatch.Stop();
	return stopwatch.Elapsed.TotalMilliseconds;
}

static double Median(List<double> samples)
{
	samples.Sort();
	return samples[samples.Count / 2];
}

internal sealed record Scenario(string Name, int[] DelaysMilliseconds);