namespace _2025_xunit_to_the_limits_src.T8_AsyncCollections_TestContainers;

[CollectionDefinition(nameof(CollectionDefinitionOfTestsWithSameContainer))]
public class CollectionDefinitionOfTestsWithSameContainer : ICollectionFixture<TestFixtureWithContainer4Mongo>
{
}

// this should speed up all tests using the collection, because one fixture is created, hence the container is "shared"