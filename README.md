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
- **T9webAPI**: ASP.NET Core Web API with testing using WebApplicationFactory

### 6. Custom DSL for Readable Tests
- **T9_SocialAsyncContainers_withDSL**: Creating domain-specific languages for more readable tests
- **Z_BDD_withCustomUnitOfWorkDsl**: Behavior-driven development with custom DSL

## Key Techniques Demonstrated

1. **Unit Testing**: Traditional isolated tests with mocks and stubs
2. **Social Testing**: Tests that share state and resources to simulate real-world usage
3. **TestContainers**: Using containerized dependencies for realistic testing environments
4. **WebApplicationFactory**: Testing ASP.NET Core APIs with in-memory servers
5. **Playwright**: Browser automation for end-to-end testing
6. **Custom DSL**: Creating fluent APIs and domain-specific languages for more expressive tests

## Repository Structure

- `/test-2025`: Main directory containing all testing examples
- `/T8_Repositories_Adapters`: Storage adapters for different database technologies
- `/T9webAPI`: Sample ASP.NET Core Web API for testing
- `/BenchmarkToTheLimits`: Performance benchmarks for testing approaches

Each example builds upon previous concepts, demonstrating increasingly sophisticated testing patterns and techniques.
