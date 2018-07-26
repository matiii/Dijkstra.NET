# Dijkstra.NET Dijkstra algorithm in C&#35; [![NuGet Version](https://img.shields.io/badge/Dijkstra.NET-1.0.9-blue.svg)](https://www.nuget.org/packages/Dijkstra.NET)

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
## Parallel version - obsolete

Library provide parallel version of finding the shortest path in graph, it uses [breadth-first search](https://en.wikipedia.org/wiki/Breadth-first_search) algorithm and Map-Reduce technics. Parallel version use all cores to find the shortest path thus processing cost is higher than classic way, however it can gives quicker result when dense graph with similar weights was processed. Simple example below.

```c#
var graph = new Graph<int, string>();

graph.AddNode(1);
graph.AddNode(2);

graph.Connect(0, 1, 5, "some custom information in edge"); //First node has key equal 0

var bfs = new BfsParallel<int, string>(graph);
IShortestPathResult result = bfs.Process(0, 1); //result contains the shortest path
result.GetPath();
```

## Benchmark

For Graph where number of nodes is 10 000 000 and connections between them 1 000 000. The length of path is minimum 10.

``` ini

BenchmarkDotNet=v0.10.10, OS=Windows 7 SP1 (6.1.7601.0)
Processor=Intel Core i7-6600U CPU 2.60GHz (Skylake), ProcessorCount=4
Frequency=2742265 Hz, Resolution=364.6621 ns, Timer=TSC
  [Host]     : .NET Framework 4.7 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.2116.0
  Job-XQYAYC : .NET Framework 4.7 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.2116.0

LaunchCount=1  RunStrategy=Monitoring  TargetCount=3  
WarmupCount=2  

```
|            Method |      Mean |    StdDev | Scaled | ScaledSD |
|------------------ |----------:|----------:|-------:|---------:|
| DijkstraBenchmark |  24.07 us |  2.188 us |   1.00 |     0.00 |
|      BfsBenchmark | 173.82 us | 19.673 us |   7.26 |     0.86 |

## License

[![License](https://img.shields.io/badge/license-MIT-blue.svg?style=plastic)](https://github.com/matiii/Dijkstra.NET/blob/master/LICENSE)

Dijkstra.NET is licensed under the MIT license. See [LICENSE](LICENSE) file for full license information.
