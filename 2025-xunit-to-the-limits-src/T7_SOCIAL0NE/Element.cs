namespace _2025_xunit_to_the_limits_src.T7_SOCIAL0NE;

public record Element
{
    public string Uid { get; init; } = NUlid.Ulid.NewUlid().ToString();

}