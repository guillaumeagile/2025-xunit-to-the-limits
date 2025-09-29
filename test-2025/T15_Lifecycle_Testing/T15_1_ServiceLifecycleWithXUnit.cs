using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace test_2025.T15_Lifecycle_Testing;

// Service that tracks its lifecycle for testing
public interface ILifecycleTrackingService
{
    string ServiceId { get; }
    DateTime CreatedAt { get; }
    bool IsDisposed { get; }
    void DoWork();
}

public class LifecycleTrackingService : ILifecycleTrackingService, IDisposable
{
    public string ServiceId { get; } = Guid.NewGuid().ToString("N")[..8];
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public bool IsDisposed { get; private set; }

    public void DoWork()
    {
        if (IsDisposed)
            throw new ObjectDisposedException(nameof(LifecycleTrackingService));
        
        Console.WriteLine($"[{ServiceId}] Working at {DateTime.UtcNow:HH:mm:ss.fff}");
    }

    public void Dispose()
    {
        if (!IsDisposed)
        {
            IsDisposed = true;
            Console.WriteLine($"[{ServiceId}] Disposed at {DateTime.UtcNow:HH:mm:ss.fff}");
        }
    }
}

// Static tracker to observe service lifecycle across tests
public static class ServiceLifecycleTracker
{
    private static readonly List<string> _events = new();
    private static readonly object _lock = new();

    public static void RecordEvent(string eventDescription)
    {
        lock (_lock)
        {
            _events.Add($"{DateTime.UtcNow:HH:mm:ss.fff} - {eventDescription}");
        }
    }

    public static List<string> GetEvents()
    {
        lock (_lock)
        {
            return new List<string>(_events);
        }
    }

    public static void Clear()
    {
        lock (_lock)
        {
            _events.Clear();
        }
    }
}

// Enhanced service that reports lifecycle events
public class ObservableLifecycleService : ILifecycleTrackingService, IDisposable
{
    public string ServiceId { get; } = Guid.NewGuid().ToString("N")[..8];
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public bool IsDisposed { get; private set; }

    public ObservableLifecycleService()
    {
        ServiceLifecycleTracker.RecordEvent($"Service {ServiceId} CREATED");
    }

    public void DoWork()
    {
        if (IsDisposed)
            throw new ObjectDisposedException(nameof(ObservableLifecycleService));
        
        ServiceLifecycleTracker.RecordEvent($"Service {ServiceId} WORKING");
    }

    public void Dispose()
    {
        if (!IsDisposed)
        {
            IsDisposed = true;
            ServiceLifecycleTracker.RecordEvent($"Service {ServiceId} DISPOSED");
        }
    }
}

// Test class demonstrating xUnit lifecycle integration with DI service lifetimes
public class T15_1_ServiceLifecycleWithXUnit : IDisposable
{
    private readonly ITestOutputHelper _output;
    private readonly ServiceProvider _serviceProvider;

    public T15_1_ServiceLifecycleWithXUnit(ITestOutputHelper output)
    {
        _output = output;
        
        // xUnit creates a new instance of this test class for EACH test method
        // This constructor runs before EACH test
        _output.WriteLine($"=== TEST CLASS CONSTRUCTOR at {DateTime.UtcNow:HH:mm:ss.fff} ===");
        
        var services = new ServiceCollection();
        services.AddTransient<ILifecycleTrackingService, ObservableLifecycleService>();
        _serviceProvider = services.BuildServiceProvider();
        
        ServiceLifecycleTracker.Clear();
        ServiceLifecycleTracker.RecordEvent("Test class constructor completed");
    }

    [Fact]
    public void TransientServices_CreateNewInstancePerResolution_WithinSameTest()
    {
        _output.WriteLine("=== TRANSIENT LIFECYCLE WITHIN SINGLE TEST ===");
        ServiceLifecycleTracker.RecordEvent("Test method started");

        // Each GetService call creates a new transient instance
        var service1 = _serviceProvider.GetService<ILifecycleTrackingService>();
        var service2 = _serviceProvider.GetService<ILifecycleTrackingService>();
        var service3 = _serviceProvider.GetService<ILifecycleTrackingService>();

        service1!.DoWork();
        service2!.DoWork();
        service3!.DoWork();

        // Verify different instances
        Assert.NotEqual(service1.ServiceId, service2.ServiceId);
        Assert.NotEqual(service2.ServiceId, service3.ServiceId);

        var events = ServiceLifecycleTracker.GetEvents();
        foreach (var evt in events)
        {
            _output.WriteLine(evt);
        }

        // Should have 3 creation events and 3 work events
        var creationEvents = events.Count(e => e.Contains("CREATED"));
        var workEvents = events.Count(e => e.Contains("WORKING"));
        
        Assert.Equal(3, creationEvents);
        Assert.Equal(3, workEvents);

        _output.WriteLine("✅ Transient services create new instances per resolution");
    }

    [Fact]
    public void ScopedServices_ShareInstanceWithinScope_DisposeAtScopeEnd()
    {
        _output.WriteLine("=== SCOPED LIFECYCLE WITH CONTROLLED DISPOSAL ===");
        ServiceLifecycleTracker.RecordEvent("Scoped test started");

        // Configure scoped services
        var services = new ServiceCollection();
        services.AddScoped<ILifecycleTrackingService, ObservableLifecycleService>();
        var provider = services.BuildServiceProvider();

        string scopedServiceId;
        
        // Create and use a scope - this controls the lifecycle
        using (var scope = provider.CreateScope())
        {
            ServiceLifecycleTracker.RecordEvent("Scope created");
            
            var service1 = scope.ServiceProvider.GetService<ILifecycleTrackingService>();
            var service2 = scope.ServiceProvider.GetService<ILifecycleTrackingService>();
            
            // Same instance within scope
            Assert.Same(service1, service2);
            scopedServiceId = service1!.ServiceId;
            
            service1.DoWork();
            service2.DoWork(); // Same instance, so same ID in logs
            
            ServiceLifecycleTracker.RecordEvent("About to exit scope");
        } // Scope disposal happens here - service should be disposed
        
        ServiceLifecycleTracker.RecordEvent("Scope exited");

        var events = ServiceLifecycleTracker.GetEvents();
        foreach (var evt in events)
        {
            _output.WriteLine(evt);
        }

        // Verify disposal occurred
        var disposalEvents = events.Where(e => e.Contains("DISPOSED")).ToList();
        Assert.Single(disposalEvents);
        Assert.Contains(scopedServiceId, disposalEvents[0]);

        _output.WriteLine("✅ Scoped service was properly disposed when scope ended");
    }

    [Fact]
    public void SingletonServices_LiveForEntireProviderLifetime()
    {
        _output.WriteLine("=== SINGLETON LIFECYCLE ACROSS MULTIPLE SCOPES ===");
        ServiceLifecycleTracker.RecordEvent("Singleton test started");

        var services = new ServiceCollection();
        services.AddSingleton<ILifecycleTrackingService, ObservableLifecycleService>();
        var provider = services.BuildServiceProvider();

        string singletonId;

        // First scope
        using (var scope1 = provider.CreateScope())
        {
            var service = scope1.ServiceProvider.GetService<ILifecycleTrackingService>();
            singletonId = service!.ServiceId;
            service.DoWork();
        }

        // Second scope - should get same singleton instance
        using (var scope2 = provider.CreateScope())
        {
            var service = scope2.ServiceProvider.GetService<ILifecycleTrackingService>();
            Assert.Equal(singletonId, service!.ServiceId);
            service.DoWork();
        }

        // Singleton should still be alive
        var directService = provider.GetService<ILifecycleTrackingService>();
        Assert.Equal(singletonId, directService!.ServiceId);
        Assert.False(directService.IsDisposed);

        var events = ServiceLifecycleTracker.GetEvents();
        foreach (var evt in events)
        {
            _output.WriteLine(evt);
        }

        // Should have only 1 creation event but multiple work events
        var creationEvents = events.Count(e => e.Contains("CREATED"));
        var workEvents = events.Count(e => e.Contains("WORKING"));
        
        Assert.Equal(1, creationEvents);
        Assert.Equal(3, workEvents); // 3 DoWork calls

        // No disposal yet - singleton lives until provider is disposed
        var disposalEvents = events.Count(e => e.Contains("DISPOSED"));
        Assert.Equal(0, disposalEvents);

        provider.Dispose(); // This will dispose the singleton

        _output.WriteLine("✅ Singleton lived across multiple scopes and was disposed with provider");
    }

    [Fact]
    public void XUnitLifecycle_CreatesNewTestInstancePerMethod()
    {
        _output.WriteLine("=== XUNIT LIFECYCLE DEMONSTRATION ===");
        _output.WriteLine("This test class instance was created specifically for this test method");
        _output.WriteLine("Each test method gets its own fresh instance of the test class");
        _output.WriteLine("This ensures test isolation at the class level");
        
        ServiceLifecycleTracker.RecordEvent($"Test method executing in test class instance");
        
        var events = ServiceLifecycleTracker.GetEvents();
        foreach (var evt in events)
        {
            _output.WriteLine(evt);
        }

        // The tracker was cleared in constructor, so we should only see events from this test
        Assert.Contains(events, e => e.Contains("Test class constructor completed"));
        
        _output.WriteLine("✅ xUnit test lifecycle provides natural isolation boundaries");
    }

    // This runs after EACH test method
    public void Dispose()
    {
        _output.WriteLine($"=== TEST CLASS DISPOSE at {DateTime.UtcNow:HH:mm:ss.fff} ===");
        ServiceLifecycleTracker.RecordEvent("Test class disposing");
        
        _serviceProvider?.Dispose();
        
        _output.WriteLine("Test class disposed - ServiceProvider cleaned up");
    }
}

// Demonstration of how test fixtures can control service lifetimes
public class ServiceLifetimeFixture : IDisposable
{
    public ServiceProvider ServiceProvider { get; }
    public DateTime FixtureCreatedAt { get; } = DateTime.UtcNow;

    public ServiceLifetimeFixture()
    {
        Console.WriteLine($"=== FIXTURE CONSTRUCTOR at {DateTime.UtcNow:HH:mm:ss.fff} ===");
        ServiceLifecycleTracker.RecordEvent("Fixture created - this spans multiple tests");
        
        var services = new ServiceCollection();
        services.AddSingleton<ILifecycleTrackingService, ObservableLifecycleService>();
        ServiceProvider = services.BuildServiceProvider();
    }

    public void Dispose()
    {
        Console.WriteLine($"=== FIXTURE DISPOSE at {DateTime.UtcNow:HH:mm:ss.fff} ===");
        ServiceLifecycleTracker.RecordEvent("Fixture disposing - all tests completed");
        ServiceProvider?.Dispose();
    }
}

// Tests using class fixture to demonstrate shared service lifetime across tests
public class T15_2_FixtureBasedLifecycle : IClassFixture<ServiceLifetimeFixture>
{
    private readonly ITestOutputHelper _output;
    private readonly ServiceLifetimeFixture _fixture;

    public T15_2_FixtureBasedLifecycle(ITestOutputHelper output, ServiceLifetimeFixture fixture)
    {
        _output = output;
        _fixture = fixture;
        _output.WriteLine($"Test using fixture created at {_fixture.FixtureCreatedAt:HH:mm:ss.fff}");
    }

    [Fact]
    public void FirstTest_UsesSharedSingleton()
    {
        _output.WriteLine("=== FIRST TEST WITH SHARED FIXTURE ===");
        
        var service = _fixture.ServiceProvider.GetService<ILifecycleTrackingService>();
        service!.DoWork();
        
        var events = ServiceLifecycleTracker.GetEvents();
        foreach (var evt in events)
        {
            _output.WriteLine(evt);
        }
        
        _output.WriteLine($"Service ID: {service.ServiceId}");
        _output.WriteLine("✅ First test completed");
    }

    [Fact]
    public void SecondTest_UsesSameSingletonInstance()
    {
        _output.WriteLine("=== SECOND TEST WITH SHARED FIXTURE ===");
        
        var service = _fixture.ServiceProvider.GetService<ILifecycleTrackingService>();
        service!.DoWork();
        
        var events = ServiceLifecycleTracker.GetEvents();
        foreach (var evt in events)
        {
            _output.WriteLine(evt);
        }
        
        _output.WriteLine($"Service ID: {service.ServiceId}");
        _output.WriteLine("✅ Second test completed - should have same service ID as first test");
    }
}
