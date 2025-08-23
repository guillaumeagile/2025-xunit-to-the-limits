using System.Diagnostics;
using FluentAssertions;
using Xbehave;

namespace _2025_xunit_to_the_limits_src.Z_BDD_withXBehave;

public class GherkinWithXBehaveTests
{
    [Scenario]
    public void Teardown_compensates_side_effects()
    {
        var created = false;
        var cleaned = false;

        "given a temporary resource".x(() => { created = true; })
            .Teardown(() => { cleaned = true; });

        "when I use the resource".x(() =>
        {
            created.Should().BeTrue("resource should exist during scenario");
        });

        "then it is cleaned after the scenario".x(() =>
        {
            cleaned.Should().BeFalse("cleanup happens after scenario end");
        });
    }

    [Scenario]
    [Example(1, 2, 3)]
    [Example(10, 20, 30)]
    [Example(-5, 7, 2)]
    public void Parameterized_outline_addition(int a, int b, int expected)
    {
        var result = 0;

        $"given a={a} and b={b}".x(() => { });

        "when I add them".x(() =>
        {
            result = a + b;
        });

        "then the sum matches the expected value".x(() =>
        {
            result.Should().Be(expected);
        });
    }

    [Scenario]
    public void Eventually_consistent_condition_with_retry()
    {
        var started = DateTime.UtcNow;
        var ready = false;

        "given an async process that becomes ready soon".x(() =>
        {
            // simulate work finishing in ~200ms
            _ = Task.Run(async () =>
            {
                await Task.Delay(200);
                ready = true;
            });
        });

        "when I wait until it is ready with a timeout".x(() =>
        {
            var timeout = TimeSpan.FromSeconds(2);
            var sw = Stopwatch.StartNew();
            while (!ready && sw.Elapsed < timeout)
            {
                Task.Delay(50).GetAwaiter().GetResult();
            }
            sw.Stop();
        });

        "then the process should be ready within the timeout".x(() =>
        {
            var elapsed = DateTime.UtcNow - started;
            ready.Should().BeTrue();
            elapsed.Should().BeLessThan(TimeSpan.FromSeconds(2));
        });
    }
}