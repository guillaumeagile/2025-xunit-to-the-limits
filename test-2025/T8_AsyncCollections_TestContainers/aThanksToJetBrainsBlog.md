https://blog.jetbrains.com/dotnet/2023/10/24/how-to-use-testcontainers-with-dotnet-unit-tests/

# TL;DR: TestContainers with .NET Unit Tests

## What is TestContainers?
- **Framework** that lets you create Docker containers programmatically in your test code
- **Replaces mocks** with real dependencies (databases, message brokers, etc.)
- **Language support** for .NET, Java, Go, Python, etc.
- **Pre-built modules** for PostgreSQL, Redis, RabbitMQ, etc.

## Setup
```bash
dotnet add package Testcontainers
# For specific services: TestContainers.PostgreSql
```

## Three xUnit Isolation Strategies

### 1. **Container-per-test** (Slowest, Most Isolated)
- New container for each `[Fact]`
- Uses `IAsyncLifetime` 
- **~13 seconds** for 3 tests
- Perfect isolation but slow

### 2. **Container-per-class** (Faster)
- One container shared across all tests in a class
- Uses `IClassFixture<DatabaseFixture>`
- **~9 seconds** for 3 tests
- Risk: tests can interfere with each other

### 3. **Container-per-collection** (Fastest)
- One container shared across multiple test classes
- Uses `[Collection("name")]` + `ICollectionFixture<>`
- **~4 seconds** for 6 tests (double the work, 1/3 the time)
- Highest performance, highest interference risk

## Best Practices
- **Group read-only tests** together for shared containers
- **Use cleanup/reset** in constructors/dispose for shared containers
- **Consider database schemas** for tenant isolation
- **Balance speed vs. isolation** based on your needs

## Key Benefits
- **Real dependencies** instead of mocks
- **Automatic container lifecycle** management
- **Wait strategies** ensure containers are ready
- **Random port binding** prevents conflicts

Perfect for integration tests where you need real databases, message queues, or other services without the complexity of manual Docker management.

