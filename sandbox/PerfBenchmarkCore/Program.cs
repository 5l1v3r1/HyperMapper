﻿using Benchmark.Flattening;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;
using HyperMapper;
using HyperMapper.Internal;
using HyperMapper.Resolvers;
using PerfBenchmark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;

class Program
{
    static void Main(string[] args)
    {
        var switcher = new BenchmarkSwitcher(new[]
        {
            typeof(MapBenchmark),
        });

        args = new string[] { "0" };

#if DEBUG

        var _foo = Foo.New();
        var hee = StandardResolver.Default.GetMapper<Foo, Foo>().Map(_foo, StandardResolver.Default);
        var heee = _foo.Adapt<Foo>();
        var foo2 = new List<int>(){ 1, 10, 100};
        var foo3 = ObjectMapper.DeepCopy(foo2);


        Console.WriteLine(foo3);

#else


        switcher.Run(args);
#endif
    }
}

public class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        Add(MarkdownExporter.GitHub);
        Add(MemoryDiagnoser.Default);

        var baseConfig = Job.ShortRun.WithLaunchCount(1).WithTargetCount(1).WithWarmupCount(1);
        // Add(baseConfig.With(Runtime.Clr).With(Jit.RyuJit).With(Platform.X64));
        //Add(baseConfig.With(Runtime.Core).With(Jit.RyuJit).With(CsProjCoreToolchain.NetCoreApp20));

        Add(baseConfig);
    }
}