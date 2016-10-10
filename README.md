# Dijkstra.NET Dijkstra algorithm in [C#]

Dijkstra algorithm which use priority queue thus complexity is equal O(ElogV) where E is number of edges and V is number of vertices. Used data structures are based on interfaces so you can implement your own or reused present. Simply example below. More information about algorithm you can find on the [wikipedia](https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm).

## Installation

From [Nuget](https://www.nuget.org/packages/Dijkstra.NET)

## Simple example

```c#
var graph = new Graph<int, string>();

graph.AddNode(1);
graph.AddNode(2);

graph.Connect(0, 1, 5, "some custom information in edge"); //First node has key equal 0

var dijkstra = new Dijkstra<int, string>(graph);
DijkstraResult result = dijkstra.Process(0, 1); //result contains the shortest path
result.GetPath();
```
## License

[![License](https://img.shields.io/badge/license-MIT-blue.svg?style=plastic)](https://github.com/matiii/Dijkstra.NET/blob/master/LICENSE)

Dijkstra.NET is licensed under the MIT license. See [LICENSE](LICENSE) file for full license information.
