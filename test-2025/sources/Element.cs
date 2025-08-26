namespace _2025_xunit_to_the_limits_src.T5_SOCIAL0NE.sources;

public record Element
{
    public string Uid { get; init; } = NUlid.Ulid.NewUlid().ToString();
}