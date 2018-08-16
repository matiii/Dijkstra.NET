# Dijkstra.NET Dijkstra algorithm in C&#35; [![NuGet Version](https://img.shields.io/badge/Dijkstra.NET-1.1.0-blue.svg)](https://www.nuget.org/packages/Dijkstra.NET)

Dijkstra algorithm which use priority queue thus complexity is equal O(ElogV) where E is number of edges and V is number of vertices. Used data structures are based on interfaces so you can implement your own or reused present. Simply example below. More information about algorithm you can find on [Wikipedia](https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm).

## Get Started
Install the latest version from NuGet

```
  Install-Package Dijkstra.NET
```

## Simple example

```c#
using Dijkstra.NET.Extensions;

var graph = new Graph<int, string>();

graph.AddNode(1);
graph.AddNode(2);

graph.Connect(0, 1, 5, "some custom information in edge"); //First node has key equal 0

ShortestPathResult result = graph.Dijkstra(0, 1); //result contains the shortest path

result.GetPath();
```

## Benchmark

For Graph where number of nodes is 10 000 000 and connections between them 1 000 000. The length of path is minimum 10.

``` ini

BenchmarkDotNet=v0.10.10, OS=Windows 7 SP1 (6.1.7601.0)
Processor=Intel Core i7-6600U CPU 2.60GHz (Skylake), ProcessorCount=4
Frequency=2742363 Hz, Resolution=364.6490 ns, Timer=TSC
  [Host]     : .NET Framework 4.7 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.2563.0
  Job-XQYAYC : .NET Framework 4.7 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.2563.0

LaunchCount=1  RunStrategy=Monitoring  TargetCount=3  
WarmupCount=2  

```
|                     Method |     Mean |    Error |   StdDev | Scaled | Allocated |
|--------------------------- |---------:|---------:|---------:|-------:|----------:|
| Dijkstra | 5.348 us | 24.43 us | 1.381 us |   1.00 |     16 KB |

## License

[![License](https://img.shields.io/badge/license-MIT-blue.svg?style=plastic)](https://github.com/matiii/Dijkstra.NET/blob/master/LICENSE)

Dijkstra.NET is licensed under the MIT license. See [LICENSE](LICENSE) file for full license information.
