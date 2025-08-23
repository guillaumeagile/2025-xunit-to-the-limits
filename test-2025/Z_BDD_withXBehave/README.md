# xBehave Scenarios Summary

This folder contains xBehave-based BDD scenarios implemented in `GherkinWithXBehaveTests.cs`.

## What’s included

- **Teardown/compensation**
  - Demonstrates using `.Teardown(...)` to clean up resources after a scenario finishes.
  - Test: `Teardown_compensates_side_effects`
  - Assertions: `created.Should().BeTrue(...)`, `cleaned.Should().BeFalse(...)` during scenario.

- **Parameterized outline**
  - Reuses one scenario for multiple inputs using `[Example]` attributes.
  - Test: `Parameterized_outline_addition(int a, int b, int expected)`
  - Assertions: `result.Should().Be(expected)`.

- **Eventual consistency (retry)**
  - Simulates asynchronous readiness and polls with a timeout.
  - Test: `Eventually_consistent_condition_with_retry`
  - Assertions: `ready.Should().BeTrue()`, `elapsed.Should().BeLessThan(...)`.

## How to run only these tests

```bash
# From repository root
 dotnet test -nologo -v minimal --filter FullyQualifiedName~Z_BDD_withXBehave.GherkinWithXBehaveTests
```

## Dependency

- Added NuGet package: `xbehave` (2.4.1) in `test-2025/test-2025.csproj`.
- Assertions use FluentAssertions (via `AwesomeAssertions` package already referenced in the project).
