```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.7.5 (23H527) [Darwin 23.6.0]
Apple M3 Max, 1 CPU, 14 logical and 14 physical cores
.NET SDK 8.0.405
  [Host] : .NET 8.0.12 (8.0.1224.60305), Arm64 RyuJIT AdvSIMD

IterationCount=10  LaunchCount=1  WarmupCount=1  

```
| Method         | Mean | Error |
|--------------- |-----:|------:|
| WithMongoAsync |   NA |    NA |

Benchmarks with issues:
  Benchmarks.WithMongoAsync: Job-GZVKPU(IterationCount=10, LaunchCount=1, WarmupCount=1)
