# Dijkstra.NET Dijkstra algorithm in C&#35; [![NuGet Version](https://img.shields.io/badge/Dijkstra.NET-1.0.7-blue.svg)](https://www.nuget.org/packages/Dijkstra.NET)

Dijkstra algorithm which use priority queue thus complexity is equal O(ElogV) where E is number of edges and V is number of vertices. Used data structures are based on interfaces so you can implement your own or reused present. Simply example below. More information about algorithm you can find on the [wikipedia](https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm).

## Get Started
Install the latest version from NuGet

```
  Install-Package Dijkstra.NET
```

## Simple example

```c#
var graph = new Graph<int, string>();

graph.AddNode(1);
graph.AddNode(2);

graph.Connect(0, 1, 5, "some custom information in edge"); //First node has key equal 0

var dijkstra = new Dijkstra<int, string>(graph);
IShortestPathResult result = dijkstra.Process(0, 1); //result contains the shortest path
result.GetPath();
```
## Parallel version

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

For Graph where number of vertices is 1 000 000 and number of edges is 10 000 000. Benchmark is available on the [benchmark](https://github.com/matiii/Dijkstra.NET/blob/benchmark/src/Dijkstra.NET/Dijkstra.NET.Benchmark/DijkstraBenchmark.cs) branch.

```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-6820HQ CPU 2.70GHz, ProcessorCount=8
Frequency=2648439 ticks, Resolution=377.5809 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=64-bit RELEASE [RyuJIT]
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=DijkstraBenchmark  Mode=SingleRun  LaunchCount=1  
WarmupCount=1  TargetCount=1  

```
  Method |      Median |    StdDev |
-------- |------------ |---------- |
 GetPath | [695.5040 us](https://www.google.pl/#q=695+us+to+sec) | 0.0000 us |

## License

[![License](https://img.shields.io/badge/license-MIT-blue.svg?style=plastic)](https://github.com/matiii/Dijkstra.NET/blob/master/LICENSE)

Dijkstra.NET is licensed under the MIT license. See [LICENSE](LICENSE) file for full license information.
