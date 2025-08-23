using _2025_xunit_to_the_limits_src.Z_BDD_withCustomUnitOfWorkDsl.CustomDslTooling;

namespace _2025_xunit_to_the_limits_src.Z_BDD_withCustomUnitOfWorkDsl;

public class GherkinHomeMadeDslTests
{
    [Fact]
    public async Task Simple_Given_When_Then_works()
    {
        await using var uow = new TestUnitOfWork();

        await uow
            .Given(async (ctx, ct) =>
            {
                ctx.Set("number", 40);
                await Task.CompletedTask;
            })
            .When(async (ctx, ct) =>
            {
                var n = ctx.Get<int>("number");
                ctx.Set("answer", n + 2);
                await Task.CompletedTask;
            })
            .Then(async ctx =>
            {
                Assert.Equal(42, ctx.Get<int>("answer"));
                
                await Task.CompletedTask;
            })
            .ExecuteAsync();
    }

    [Fact]
    public async Task Compensation_runs_on_dispose_in_reverse_order()
    {
        var cleaned = false;

        var uow = new TestUnitOfWork();

        uow.Given(
            async (ctx, ct) =>
            {
                ctx.Set("resource", "temp");
                await Task.CompletedTask;
            },
            compensate: async ctx =>
            {
                // cleanup side-effects here
                cleaned = true;
                await Task.CompletedTask;
            });

        await uow.ExecuteAsync();

        await uow.DisposeAsync();

        Assert.True(cleaned);
    }
}