namespace Dijkstra.NET.ShortestPath.Utility
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;
    using Contract;
    using Model;
    using Timer = System.Timers.Timer;

    internal sealed class ProducerConsumer<T, TEdgeCustom> : IDisposable where TEdgeCustom : IEquatable<TEdgeCustom>
    {
        private readonly BlockingCollection<IConcurrentNode<T, TEdgeCustom>> _mapper = new BlockingCollection<IConcurrentNode<T, TEdgeCustom>>(new ConcurrentBag<IConcurrentNode<T, TEdgeCustom>>()); //todo: concern collection
        private readonly BlockingCollection<MapReduceJob> _reducer = new BlockingCollection<MapReduceJob>(new ConcurrentBag<MapReduceJob>());

        private readonly Timer _guardTimer = new Timer(1000);
        private readonly Timer _checkerGuardTimer = new Timer(50);

        private int _producersCounter;
        private int _consumersCounter;

        private bool _isDisposed;
        private int _guardIsWorking;

        private volatile int _counter;

        public int Insurance { get; set; } = 20;

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

        private void StartGuard()
        {
            if (Interlocked.Exchange(ref _guardIsWorking, 1) == 0)
                Guard();
        }

        private bool IsConsuming => _consumersCounter > 0;
        private bool IsProducing => _producersCounter > 0;

        private void Guard()
        {
            bool firstElapsed = true;

            _guardTimer.Elapsed += (sender, args) =>
            {
                if (firstElapsed)
                {
                    firstElapsed = false;
                    return;
                }

                if (IsNotWorking)
                {
                    _guardTimer.Stop();
                    _checkerGuardTimer.Start();
                }
            };

            _checkerGuardTimer.Elapsed += (sender, args) =>
            {
                if (_counter == Insurance)
                {
                    _checkerGuardTimer.Stop();

                    ProduceComplete();
                    ConsumeComplete();
                } else if (IsNotWorking)
                {
                    _counter++;
                }
                else
                {
                    _checkerGuardTimer.Stop();
                    _counter = 0;
                    _guardTimer.Start();
                }
            };

            _guardTimer.Start();
        }

        private bool IsNotWorking => !IsConsuming && !IsProducing && _mapper.Count == 0 && _reducer.Count == 0;

        public void Producing(Action<IConcurrentNode<T, TEdgeCustom>> process)
        {
            StartGuard();
            Parallel.ForEach(_mapper.GetConsumingEnumerable(), node =>
            {
                Interlocked.Increment(ref _producersCounter);
                process(node);
                Interlocked.Decrement(ref _producersCounter);
            });
        }

        public void Consuming(Action<MapReduceJob> process)
        {
            StartGuard();
            Parallel.ForEach(_reducer.GetConsumingEnumerable(), job =>
            {
                Interlocked.Increment(ref _consumersCounter);
                process(job);
                Interlocked.Decrement(ref _consumersCounter);
            });
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _guardTimer.Dispose();
                _checkerGuardTimer.Dispose();
                _isDisposed = true;
            }
        }
    }
}
