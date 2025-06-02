using System;
using System.Threading.Tasks;
using _2025_xunit_to_the_limits_src.T10_Redis_ToBenchmark;
using _2025_xunit_to_the_limits_src.T8_AsyncCollections_TestContainers;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using Xunit.Sdk;

namespace BenchmarkToTheLimits
{
    [SimpleJob(launchCount: 1, warmupCount: 1, iterationCount: 10)]
   // [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class Benchmarks
    {
        private TestFixtureWithContainer4Mongo fixtureMongo;
        private TestFixtureWithContainer4Redis fixtureRedis;

        [Benchmark]
        public async Task WithMongoAsync()
        {
            var test = new MyTestsWithContainers(fixtureMongo, new TestOutputHelper());
            await test.InitializeAsync();
           await test.SaveSomeDto_ToMongoDB();
        
        }

        [Benchmark]
        public async Task WithRedis()
        {
            var test = new RedisTestsWithContainers(fixtureRedis, new TestOutputHelper());
            await test.InitializeAsync();
            await test.InsertAndGetByIdAsync_ShouldWorkCorrectly();
        }
        
        [GlobalSetup()]
        public void FirstGlobalSetup()
        {
            fixtureMongo = new TestFixtureWithContainer4Mongo();
            fixtureMongo.InitializeAsync().Wait();
            
            fixtureRedis = new TestFixtureWithContainer4Redis();
             fixtureRedis.InitializeAsync().Wait();
        }
        
        [GlobalCleanup()]
        public void FirstGlobalCleanup()
        {
            fixtureMongo.DisposeAsync().Wait();
            fixtureRedis.DisposeAsync().Wait();
        }
    }
}