# .NET Days 2025 - xUnit to the Limits

By Guillaume SAINT ETIENNE.

This repository contains examples demonstrating advanced testing techniques with xUnit.net, exploring what's possible beyond basic unit testing.

## Getting Started

All code examples are located in the `/test-2025` directory. Each subdirectory represents a different testing concept or technique:

## Testing Progression

### 1. Basic Unit Testing
- **T2_SolitarySync**: Ordinary synchronous unit tests with proper isolation
- **T3_Fixtures**: Using xUnit fixtures for test setup and teardown

### 2. Asynchronous Testing
- **T4_ASYNC**: Asynchronous testing patterns

### 3. Social Tests (Integration-style testing with shared state)
- **T6_SocialWithCollections**: Basic social tests using shared collections
- **T7_SocialAsyncCollections**: Asynchronous social tests with shared state

### 4. Advanced Social Tests with TestContainers
- **T8_AsyncCollections_TestContainers**: Using TestContainers for isolated database testing
- **T9_SocialAsyncContainers_HTTP**: HTTP API testing with containerized dependencies

### 5. Web API Testing
- **T9webAPI**: ASP.NET Core Web API with testing using WebApplicationFactory and Playwright (instead of http client).

### 6. Custom DSL for Readable Tests
- **T9_SocialAsyncContainers_withDSL**: Creating domain-specific languages for more readable tests
- **Z_BDD_withCustomUnitOfWorkDsl**: Behavior-driven development with custom DSL

## Key Techniques Demonstrated

1. **Unit Testing**: Traditional, solitary, isolated tests 
2. **Social Testing**: Tests that share state and resources to target classes that depends on other classes. Introduces the concept of "social tests". Demonstrate the need of fixtures to inject dependencies in tests (without ServiceProvider).
3. **TestContainers**: Using containerized dependencies for realistic testing environments
4. **WebApplicationFactory**: Testing ASP.NET Core APIs with in-memory servers, ServiceProvider is necessary at that point.
5. **Playwright**: cURL like automation for API testing
6. **Custom DSL**: Creating fluent APIs and domain-specific languages for more expressive and readable tests 

## Repository Structure

- `/test-2025`: Main directory containing all testing examples
- `/T8_Repositories_Adapters`: Source Storage adapters for different database technologies
- `/T9webAPI`: Source Sample ASP.NET Core Web API for testing
- `/BenchmarkToTheLimits`: Performance benchmarks for testing approaches (not covered in the presentation)

Each example builds upon previous concepts, demonstrating increasingly sophisticated testing patterns and techniques.
