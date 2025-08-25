using System.Text.Json;
using _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_withDSL.TestCollections;
using _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_withDSL.Tooling;
using AwesomeAssertions;
using Microsoft.Playwright;
using T8_Repositories_Adapters.source;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_withDSL;

// give this test a BDD style : given, when, then

[Collection(nameof(SharedDslCollection))]
public class WithDslGerkhinStyleTests : IClassFixture<DslFixture>, IAsyncLifetime
{
    // hide the plumbing from the tests
    private readonly DslFixture _fixtureDsl;

    public WithDslGerkhinStyleTests(DslFixture fixtureDsl, ITestOutputHelper outputHelper)
    {
        _fixtureDsl = fixtureDsl;
        fixtureDsl.TestLogger = outputHelper.ToLogger<WithDslTests>();
    }

    
    [Fact]
    public async Task Given_StoredItem_When_RequestingById_Then_ShouldReturnCorrectItem()
    {
        // GIVEN: A stored item in the database
        var expectedDto = new SomeDto("42", "Foobar", 42);
        await Given_AnItemIsStoredInDatabase(expectedDto);
        
        // WHEN: Requesting the item by its ID
        var actualDto = await When_RequestingItemById("42");
        
        // THEN: The correct item should be returned
        Then_ItemShouldMatch(actualDto, expectedDto);
    }

    private async Task Given_AnItemIsStoredInDatabase(SomeDto itemToStore)
    {
        await _fixtureDsl.InsertSome(itemToStore);
    }

    private async Task<SomeDto> When_RequestingItemById(string id)
    {
        await using var result = await _fixtureDsl
            .SetRelativePathTo($"stored/{id}")
            .GetAllAsync();
        
        var jsonElement = await result.ExtractJsonAsync();
        jsonElement.HasValue.Should().BeTrue("API should return a valid response");
        
        var actualDto = JsonSerializer.Deserialize<SomeDto>(jsonElement.ToString());
        return actualDto!;
    }

    private static void Then_ItemShouldMatch(SomeDto actual, SomeDto expected)
    {
        actual.Should().BeEquivalentTo(expected, "The returned item should match the stored item");
    }

    [Fact]
    public async Task Given_NoStoredItem_When_RequestingNonExistentId_Then_ShouldReturn404()
    {
        // GIVEN: No item with ID "999" exists in the database
        // (Database starts clean for each test)
        
        // WHEN: Requesting a non-existent item
        var response = await When_RequestingNonExistentItem("999");
        
        // THEN: Should return 404 Not Found
        Then_ShouldReturn404(response);
    }

    private async Task<IAPIResponse> When_RequestingNonExistentItem(string nonExistentId)
    {
        await using var result = await _fixtureDsl
            .SetRelativePathTo($"stored/{nonExistentId}")
            .GetAllAsync();
        
        return result.Response;
    }

    private static void Then_ShouldReturn404(IAPIResponse response)
    {
        response.Status.Should().Be(404, "Non-existent items should return 404 Not Found");
    }

    [Fact]
    public async Task Given_MultipleStoredItems_When_RequestingSpecificId_Then_ShouldReturnOnlyThatItem()
    {
        // GIVEN: Multiple items are stored in the database
        var item1 = new SomeDto("100", "Alice", 25);
        var item2 = new SomeDto("200", "Bob", 30);
        var item3 = new SomeDto("300", "Charlie", 35);
        
        await Given_MultipleItemsAreStored(item1, item2, item3);
        
        // WHEN: Requesting a specific item by ID
        var actualDto = await When_RequestingItemById("200");
        
        // THEN: Only the requested item should be returned
        Then_ItemShouldMatch(actualDto, item2);
    }

    private async Task Given_MultipleItemsAreStored(params SomeDto[] items)
    {
        foreach (var item in items)
        {
            await _fixtureDsl.InsertSome(item);
        }
    }
    async Task IAsyncLifetime.InitializeAsync()
    {
        await this._fixtureDsl.InitializeAsync(); // HERE !!!! super important to AWAIT for this
       
    }
    async Task IAsyncLifetime.DisposeAsync()
    {
        await _fixtureDsl.DisposeAsync();
    }
}