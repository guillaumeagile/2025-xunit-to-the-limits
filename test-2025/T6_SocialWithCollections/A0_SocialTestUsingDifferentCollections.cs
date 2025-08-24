using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE.sources;
using AwesomeAssertions;

namespace _2025_xunit_to_the_limits_src.T6_SocialWithCollections;

 [Collection(nameof(CollectionOfTestsWithFake))] //we force to use the broken fake
//  [Collection(nameof(CollectionOfTestsWithDriver))]   // now try with the 'nice' driver
public class A0_SocialTestUsingDifferentCollections : IClassFixture<TestFixtureWithAnyRepo>
{
    private readonly MyService sut;

    //public A0_SocialTestCollectable(TestFixtureWithFake fixture)
       public A0_SocialTestUsingDifferentCollections(TestFixtureWithFake fixture)
    {
        sut = new MyService(fixture.Repository);
    }

    

    [Fact]
    public void SocialPalGivenByFixture()
    {
        var anElement = new Element();

        var resOfSave = sut.SaveSocial(anElement);

        resOfSave.Should().BeFalse("because i'm using fake");
    }
}

//other benefit of the collection:  xUnit collection fixture is created once per test collection
//and reused by all test classes that declare that collection, for the duration of that collection’s tests.
// Details
// Scope: Per test collection (within a single test assembly).
// Reuse: One instance shared by every test class marked with [Collection("YourCollection")] .
// Lifetime: Created before the first test in the collection runs; disposed after the last test in the collection completes.
// Parallelism: Collections are the unit of parallelization. Tests in different collections may run in parallel; tests within the same collection do not, so your shared fixture won’t be concurrently used by multiple classes.