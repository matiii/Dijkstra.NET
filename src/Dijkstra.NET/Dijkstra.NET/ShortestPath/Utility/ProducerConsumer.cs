namespace Dijkstra.NET.ShortestPath.Utility
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;
    using Contract;
    using Model;

    internal class ProducerConsumer<T, TEdgeCustom> where TEdgeCustom : IEquatable<TEdgeCustom>
    {
        private readonly BlockingCollection<IConcurrentNode<T, TEdgeCustom>> _mapper = new BlockingCollection<IConcurrentNode<T, TEdgeCustom>>(new ConcurrentBag<IConcurrentNode<T, TEdgeCustom>>()); //todo: concern collection
        private readonly BlockingCollection<MapReduceJob> _reducer = new BlockingCollection<MapReduceJob>(new ConcurrentBag<MapReduceJob>());

        public ProducerConsumer()
        {
            Guard();
        }

        public void Produce(IConcurrentNode<T, TEdgeCustom> product)
        {
            _mapper.Add(product);
        }

        public void Consume(MapReduceJob product)
        {
            _reducer.Add(product);
        }

        public void ProduceComplete() => _mapper.CompleteAdding();
        public void ConsumeComplete() => _reducer.CompleteAdding();

        private void Guard()
        {
            new Task(() =>
            {
                var spin = new SpinWait();

                while (true)
                {
                    if (spin.Count == 6)
                        break;

                    if (_mapper.Count == 0 && _reducer.Count == 0)
                        spin.SpinOnce();
                }

            }).Start();
        }

        public void Producing(Action<IConcurrentNode<T, TEdgeCustom>> process)
        {
            Parallel.ForEach(_mapper.GetConsumingEnumerable(), process);
        }

        public void Consuming(Action<MapReduceJob> process)
        {
            Parallel.ForEach(_reducer.GetConsumingEnumerable(), process);
        }
    }
}
