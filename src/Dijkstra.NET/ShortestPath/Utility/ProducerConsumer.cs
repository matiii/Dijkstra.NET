namespace Dijkstra.NET.ShortestPath.Utility
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;
    using Contract;
    using Model;

    internal sealed class ProducerConsumer<T, TEdgeCustom> where TEdgeCustom : IEquatable<TEdgeCustom>
    {
        private readonly BlockingCollection<Emitter> _table = new BlockingCollection<Emitter>(50);

        private int _currentJobs;

        public Action<INode<T, TEdgeCustom>> Producing { get; set; }
        public Action<MapReduceJob> Consuming { get; set; }
        public Action Initialise { get; set; }

        public void Produce(INode<T, TEdgeCustom> product)
        {
            Emit(new Emitter(product));
        }

        public void Consume(MapReduceJob product)
        {
            Emit(new Emitter(product));
        }

        private void Emit(Emitter emitter)
        {
            Interlocked.Increment(ref _currentJobs);
            _table.Add(emitter);
        }

        public void Work()
        {
            Initialise();

            foreach (var emitter in _table.GetConsumingEnumerable())
            {
                Task.Factory.StartNew(() =>
                {
                    if (emitter.IsMapper)
                        Producing(emitter.Mapper);
                    else
                        Consuming(emitter.Reducer);

                    Interlocked.Decrement(ref _currentJobs);

                    NotifyGuard();
                });
            }
        }

        public void Complete() => _table.CompleteAdding();

        private void NotifyGuard()
        {
            if (IsNotWorking)
                Complete();
        }

        private bool IsNotWorking => _table.Count == 0 && Interlocked.CompareExchange(ref _currentJobs, 0, 0) == 0;

        private class Emitter
        {
            private readonly INode<T, TEdgeCustom> _mapper;
            private readonly MapReduceJob _reducer;

            public bool IsMapper { get; private set; }

            public Emitter(INode<T, TEdgeCustom> node)
            {
                _mapper = node;
                IsMapper = true;
            }

            public Emitter(MapReduceJob job)
            {
                _reducer = job;
            }

            public INode<T, TEdgeCustom> Mapper => _mapper;
            public MapReduceJob Reducer => _reducer;
        }
    }
}
