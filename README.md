# Dijkstra.NET [![NuGet Version](https://img.shields.io/badge/Dijkstra.NET-1.2.1-blue.svg)](https://www.nuget.org/packages/Dijkstra.NET)

Dijkstra algorithm which use priority queue thus complexity is equal O(ElogV) where E is number of edges and V is number of vertices. Used data structures are based on interfaces so you can implement your own or reused present. Simply example below. More information about algorithm you can find on [Wikipedia](https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm).

## Get Started
Install the latest version from NuGet

```
  Install-Package Dijkstra.NET
```

## Simple example

```c#
using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;

var graph = new Graph<int, string>();

graph.AddNode(1);
graph.AddNode(2);

graph.Connect(1, 2, 5, "some custom information in edge"); //First node has key equal 1

ShortestPathResult result = graph.Dijkstra(1, 2); //result contains the shortest path

var path = result.GetPath();
```
or

```c#
using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;

var graph = new Graph<int, string>() + 1 + 2;

bool connected = graph >> 1 >> 2 >> 5 ^ "custome edge information"; 

ShortestPathResult result = graph.Dijkstra(1, 2); //result contains the shortest path

var path = result.GetPath();
```

## Benchmark

For Graph where number of nodes is 1 000 000 and connections between them 1 000 000. The length of path is minimum 10.

``` ini

BenchmarkDotNet=v0.10.10, OS=Windows 10.0.17134
Processor=Intel Core i7-4700HQ CPU 2.40GHz (Haswell), ProcessorCount=8
Frequency=2338342 Hz, Resolution=427.6534 ns, Timer=TSC
  [Host]     : .NET Framework 4.6.1 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3260.0
  Job-XQYAYC : .NET Framework 4.6.1 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3260.0

LaunchCount=1  RunStrategy=Monitoring  TargetCount=3  
WarmupCount=2  

```
|                     Method |         Mean |      Error |     StdDev | Scaled | ScaledSD |      Gen 0 |      Gen 1 |   Allocated |
|--------------------------- |-------------:|-----------:|-----------:|-------:|---------:|-----------:|-----------:|------------:|
| Dijkstra |     7.515 ms |   3.845 ms |  0.2173 ms |   1.00 |     0.00 |          - |          - |       48 KB |
| PageRank | 1,139.983 ms | 762.417 ms | 43.0780 ms | 151.77 |     5.86 | 40000 | 40000 | 272365.8 KB |


## License

[![License](https://img.shields.io/badge/license-MIT-blue.svg?style=plastic)](https://github.com/matiii/Dijkstra.NET/blob/master/LICENSE)

Dijkstra.NET is licensed under the MIT license. See [LICENSE](LICENSE) file for full license information.
