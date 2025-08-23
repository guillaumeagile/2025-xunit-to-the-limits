namespace _2025_xunit_to_the_limits_src.Z_BDD_withCustomUnitOfWorkDsl.CustomDslTooling;

internal interface IScenarioStep
{
    Task ExecuteAsync(ScenarioContext ctx, CancellationToken ct);
    Task CompensateAsync(ScenarioContext ctx);
}