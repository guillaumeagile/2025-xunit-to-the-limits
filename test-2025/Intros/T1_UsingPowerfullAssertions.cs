using FluentAssertions;

namespace _2025_xunit_to_the_limits_src.Intros;

public class T1_UsingPowerfullAssertions
{
    [Fact]
    public void StringTest()
    {
        var s = "earth is flat";
        Assert.Equal("earth is flat", s);
    }
    
    
    [Fact]
    public void TypeTest()
    {
        object theObject = null;
        theObject.Should().BeNull("because the value is null");
        //    theObject.Should().NotBeNull();

        
        
        
        
        
        
        theObject = "whatever";
        theObject.Should().BeOfType<string>("because a {0} is set", typeof(string));
        theObject.Should().BeOfType(typeof(string), "because a {0} is set", typeof(string));

        theObject.Should().BeOfType<string>().Subject.Should().Be("whatever");
    }


    [Fact]
    public void CollectionsTest()
    {
        IEnumerable<int> collection = new[] { 1, 2, 5, 8 };

        collection.Should().NotBeEmpty()
            .And.HaveCount(4)
            .And.ContainInOrder(new[] { 2, 5 })
            .And.ContainItemsAssignableTo<int>();
        
        collection.Should().HaveCount(c => c > 3)
            .And.OnlyHaveUniqueItems();
        
        collection.Should().StartWith(1);
        collection.Should().StartWith(new[] { 1, 2 });
        collection.Should().EndWith(8);
        collection.Should().EndWith(new[] { 5, 8 });

        collection.Should().BeSubsetOf(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, });

//        collection.Should().ContainSingle();
        // collection.Should().ContainSingle(x => x > 3);
        collection.Should().Contain(8)
            .And.HaveElementAt(2, 5)
            .And.NotBeSubsetOf(new[] { 11, 56 });

        collection.Should().Contain(x => x > 3);
        collection.Should().Contain(collection, "", 5, 6); // It should contain the original items, plus 5 and 6.

        collection.Should().OnlyContain(x => x < 10);
        collection.Should().ContainItemsAssignableTo<int>();
        collection.Should().NotContainItemsAssignableTo<string>();

        collection.Should().ContainInOrder(new[] { 1, 5, 8 });
        collection.Should().NotContainInOrder(new[] { 5, 1, 2 });

        collection.Should().ContainInConsecutiveOrder(new[] { 2, 5, 8 });
        collection.Should().NotContainInConsecutiveOrder(new[] { 1, 5, 8 });

        collection.Should().NotContain(82);
        collection.Should().NotContain(new[] { 82, 83 });
        collection.Should().NotContainNulls();
        collection.Should().NotContain(x => x > 10);

        object boxedValue = 2;
        collection.Should().ContainEquivalentOf(boxedValue); // Compared by object equivalence
        object unexpectedBoxedValue = 82;
        collection.Should().NotContainEquivalentOf(unexpectedBoxedValue); // Compared by object equivalence

        const int element = 5;
        const int successor = 8;
        const int predecessor = 2;
        collection.Should().HaveElementPreceding(successor, element);
        collection.Should().HaveElementSucceeding(predecessor, element);


        IEnumerable<int> otherCollection = new[] { 1, 2, 5, 8, 1 };
        IEnumerable<int> anotherCollection = new[] { 10, 20, 50, 80, 10 };
        collection.Should().IntersectWith(otherCollection);
        collection.Should().NotIntersectWith(anotherCollection);

        var singleEquivalent = new[] { new { Size = 42 } };
        singleEquivalent.Should().ContainSingle()
            .Which.Should().BeEquivalentTo(new { Size = 42 });
    }

    [Fact]
    public void PleaseNoLogicInTests()
    {
        var func = (int a, int b) => a + b;

        List<int> differentCases = [10, 20];

        foreach (var cas in differentCases)
        {
            var actualResult = func(cas, cas);
            actualResult.Should().BeGreaterThan(10);

            if (cas == 10) actualResult.Should().Be(20);
        }
    }

    [Theory]
    [InlineData(10, 20)]
    [InlineData(20, 40)]
    public void TheoryIsWhatYouNeed(int a, int expected)
    {
        var func = (int x, int y) => x + y;

        var actualResult = func(a, a);
        actualResult.Should().BeGreaterThan(a);
        actualResult.Should().Be(expected);
    }
}