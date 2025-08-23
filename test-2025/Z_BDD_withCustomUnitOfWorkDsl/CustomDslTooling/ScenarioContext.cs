namespace _2025_xunit_to_the_limits_src.Z_BDD_withCustomUnitOfWorkDsl.CustomDslTooling;

public sealed class ScenarioContext
{
    private readonly Dictionary<string, object?> _bag = new();

    public T Get<T>(string key) => (T)_bag[key]!;

    public ScenarioContext Set<T>(string key, T value)
    {
        _bag[key] = value;
        return this;
    }

    public bool TryGet<T>(string key, out T value)
    {
        if (_bag.TryGetValue(key, out var v) && v is T t)
        {
            value = t;
            return true;
        }

        value = default!;
        return false;
    }
}