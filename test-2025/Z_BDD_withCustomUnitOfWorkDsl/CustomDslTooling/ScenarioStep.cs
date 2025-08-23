namespace _2025_xunit_to_the_limits_src.Z_BDD_withCustomUnitOfWorkDsl.CustomDslTooling;

internal sealed class ScenarioStep : IScenarioStep
{
    private readonly Func<ScenarioContext, CancellationToken, Task> _do;
    private readonly Func<ScenarioContext, Task>? _undo;

    public ScenarioStep(
        Func<ScenarioContext, CancellationToken, Task> @do,
        Func<ScenarioContext, Task>? undo = null)
    {
        _do = @do;
        _undo = undo;
    }

    public Task ExecuteAsync(ScenarioContext ctx, CancellationToken ct) => _do(ctx, ct);

    public Task CompensateAsync(ScenarioContext ctx) => _undo?.Invoke(ctx) ?? Task.CompletedTask;
}