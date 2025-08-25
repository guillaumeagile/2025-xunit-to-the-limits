using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE;
using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE.sources;

namespace _2025_xunit_to_the_limits_src.T6_SocialWithCollections;

[CollectionDefinition(nameof(CollectionOfTestsWithFake))]
public class CollectionOfTestsWithFake : ICollectionFixture<TestFixtureWithFake>
{
}

[CollectionDefinition(nameof(AnotherCollectionOfTestsWithFake))] //same fixture
public class AnotherCollectionOfTestsWithFake : ICollectionFixture<TestFixtureWithFake>
{
}


// will be used when removing the comment in A0_SocialTestCollectable
[CollectionDefinition(nameof(CollectionOfTestsWithDriver))]
public class CollectionOfTestsWithDriver : ICollectionFixture<TestFixtureWithDriver>
{
}

