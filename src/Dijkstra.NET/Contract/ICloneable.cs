namespace Dijkstra.NET.Contract
{
    public interface ICloneable<out T>
    {
        T Clone();
    }
}
