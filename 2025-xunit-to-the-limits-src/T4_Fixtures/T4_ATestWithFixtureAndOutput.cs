using _2025_xunit_to_the_limits_src.Intros;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T4_Fixtures;
/*
 *
    _____                   ____                       _   _     _              
   |_   __   _ _ __   ___  / ___|  ___  _ __ ___   ___| |_| |__ (_)_ __   __ _  
     | || | | | '_ \ / _ \ \___ \ / _ \| '_ ` _ \ / _ | __| '_ \| | '_ \ / _` | 
     | || |_| | |_) |  __/  ___) | (_) | | | | | |  __| |_| | | | | | | | (_| | 
     |_| \__, | .__/ \___| |____/ \___/|_| |_| |_|\___|\__|_| |_|_|_| |_|\__, | 
         |___/|_|                                                        |___/  

 *
 * 
   d888888b db    db d8888b. d88888b      .d8888.  .d88b.  .88b  d88. d88888b d888888b db   db d888888b d8b   db  d888b       
   `~~88~~' `8b  d8' 88  `8D 88'          88'  YP .8P  Y8. 88'YbdP`88 88'     `~~88~~' 88   88   `88'   888o  88 88' Y8b      
      88     `8bd8'  88oodD' 88ooooo      `8bo.   88    88 88  88  88 88ooooo    88    88ooo88    88    88V8o 88 88           
      88       88    88~~~   88~~~~~        `Y8b. 88    88 88  88  88 88~~~~~    88    88~~~88    88    88 V8o88 88  ooo      
      88       88    88      88.          db   8D `8b  d8' 88  88  88 88.        88    88   88   .88.   88  V888 88. ~8~      
      YP       YP    88      Y88888P      `8888Y'  `Y88P'  YP  YP  YP Y88888P    YP    YP   YP Y888888P VP   V8P  Y888P       
                                                                                                                              
 https://patorjk.com/software/taag/#p=testall&h=3&v=1&f=Rectangles&t=Type%20Something%20                                                                                                                             
   
 */
public class T4_ATestWithFixtureAndOutput : IClassFixture<SimpleSyncLifeTimeFixture>
{
    private readonly SimpleSyncLifeTimeFixture _fixture;

    [Fact]
    public void Test1()
    {
        _fixture.TestableValue.Should().Be(42);
    }


    [Fact]
    public void TestClassExpectingLoggerButWhere()
    {
        var sutClass = new T4_SutClass(null);
        sutClass.TestableValue.Should().Be(88);
    }
    
    [Fact]
    public void TestClassExpectingLogger()
    {
        var sutClass = new T4_SutClass(this.TestLogger);
        sutClass.TestableValue.Should().Be(88);
    }

    // we need to have that fixture
    public T4_ATestWithFixtureAndOutput(SimpleSyncLifeTimeFixture fixture, ITestOutputHelper outputHelper)
    {
        outputHelper.WriteLine("welcome in T4_ATestWithFixtureAndOutput");
        _fixture = fixture;
        outputHelper.WriteLine("fixture.TestableValue is " + fixture.TestableValue.ToString());

        
        
        
        
        
        
        
        // outputHelper is not ILogger 🤔
      //   this.TestLogger = outputHelper.ToLogger<SimpleSyncLifeTimeFixture>();
    }

    public ILogger TestLogger { get; init; }





    [Fact]
    [Trait("MyCategories", "T4_Fixtures")]
    public void Test1_Mod()
    {
        _fixture.TestableValue++;  // NOOOOOOOOOO
        _fixture.TestableValue.Should().Be(43);
    }
}