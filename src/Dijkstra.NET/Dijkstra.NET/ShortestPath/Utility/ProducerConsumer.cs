namespace Dijkstra.NET.ShortestPath.Utility
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using Contract;
    using Model;
    using Timer = System.Timers.Timer;

    internal sealed class ProducerConsumer<T, TEdgeCustom> : IDisposable where TEdgeCustom : IEquatable<TEdgeCustom>
    {
        private readonly Collection<Emitter> _table = new Collection<Emitter>();
        private readonly Timer _guardTimer;

        private bool _isDisposed;
        private int _guardIsWorking;

        private int _currentJobs;

        private int _counter;

        public ProducerConsumer(double guardInterval = 20)
        {
            _guardTimer = new Timer(guardInterval);

            _guardTimer.Elapsed += (sender, args) =>
            {
                if (Interlocked.CompareExchange(ref _counter, Insurance, Insurance) == Insurance)
                {
                    _guardTimer.Stop();
                    Complete();
                }
                else if (IsNotWorking)
                    Interlocked.Increment(ref _counter);
                else
                    Interlocked.Exchange(ref _counter, 0);
            };
        }

        public int Insurance { get; set; } = 20;

        public Action<IConcurrentNode<T, TEdgeCustom>> Producing { get; set; }
        public Action<MapReduceJob> Consuming { get; set; }

        public void Produce(IConcurrentNode<T, TEdgeCustom> product)
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
            if (Interlocked.Exchange(ref _guardIsWorking, 1) == 0)
                _guardTimer.Start();
            else
                Interlocked.Exchange(ref _counter, 0);
        }

        private bool IsNotWorking => _table.Count == 0 && Interlocked.CompareExchange(ref _currentJobs, 0, 0) == 0;

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _guardTimer.Dispose();
                _isDisposed = true;
            }
        }

        private class Emitter
        {
            private readonly IConcurrentNode<T, TEdgeCustom> _mapper;
            private readonly MapReduceJob _reducer;

            public bool IsMapper { get; private set; }

            public Emitter(IConcurrentNode<T, TEdgeCustom> node)
            {
                _mapper = node;
                IsMapper = true;
            }

            public Emitter(MapReduceJob job)
            {
                _reducer = job;
            }

            public IConcurrentNode<T, TEdgeCustom> Mapper => _mapper;
            public MapReduceJob Reducer => _reducer;
        }
    }
}
