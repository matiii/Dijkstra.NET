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
        private readonly BlockingCollection<int> _guardSchedule = new BlockingCollection<int>();

        private int _producersCounter;
        private int _consumersCounter;


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

        public void StartGuard() => Guard();


        public void NotifyGuard()
        {
            if (!_guardSchedule.IsAddingCompleted && !IsConsuming && !IsProducing && _mapper.Count == 0 && _reducer.Count == 0)
            {
                _guardSchedule.Add(1);
            }
        }

        private bool IsConsuming => _consumersCounter > 0;
        private bool IsProducing => _producersCounter > 0;

        private void Guard()
        {
            new Task(() =>
            {
               int _ = _guardSchedule.Take();

                if (!IsConsuming && !IsProducing && _mapper.Count == 0 && _reducer.Count == 0)
                {
                    ProduceComplete();
                    ConsumeComplete();
                    
                    _guardSchedule.CompleteAdding();
                }

            }).Start();
        }



        public void Producing(Action<IConcurrentNode<T, TEdgeCustom>> process)
        {
            Parallel.ForEach(_mapper.GetConsumingEnumerable(), node =>
            {
                Interlocked.Increment(ref _producersCounter);
                process(node);
                Interlocked.Decrement(ref _producersCounter);
            });
        }

        public void Consuming(Action<MapReduceJob> process)
        {
            Parallel.ForEach(_reducer.GetConsumingEnumerable(), job =>
            {
                Interlocked.Increment(ref _consumersCounter);
                process(job);
                Interlocked.Decrement(ref _consumersCounter);
                NotifyGuard();
            });
        }
    }
}
