namespace _2025_xunit_to_the_limits_src.Z_BDD_withCustomUnitOfWorkDsl.CustomDslTooling;

public sealed class TestUnitOfWork : IAsyncDisposable
{
    private readonly List<IScenarioStep> _steps = new();
    private readonly List<IScenarioStep> _executed = new();

    public ScenarioContext Context { get; } = new();

    public TestUnitOfWork Given(
        Func<ScenarioContext, CancellationToken, Task> step,
        Func<ScenarioContext, Task>? compensate = null)
    {
        _steps.Add(new ScenarioStep(step, compensate));
        return this;
    }

    public TestUnitOfWork When(
        Func<ScenarioContext, CancellationToken, Task> step,
        Func<ScenarioContext, Task>? compensate = null)
    {
        _steps.Add(new ScenarioStep(step, compensate));
        return this;
    }

    public TestUnitOfWork Then(
        Func<ScenarioContext, Task> assertion)
    {
        _steps.Add(new ScenarioStep(async (c, _) => await assertion(c)));
        return this;
    }

    public async Task ExecuteAsync(CancellationToken ct = default)
    {
        foreach (var s in _steps)
        {
            await s.ExecuteAsync(Context, ct);
            _executed.Add(s);
        }
    }

    public async ValueTask DisposeAsync()
    {
        for (var i = _executed.Count - 1; i >= 0; i--)
        {
            await _executed[i].CompensateAsync(Context);
        }
    }
}