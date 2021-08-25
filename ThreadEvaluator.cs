using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Otus28
{
    public class ThreadEvaluator : IEValuator
    {
        private readonly ConcurrentBag<int> _results = new();

        private readonly int _threadsCount;

        public ThreadEvaluator(int threadsCount)
        {
            _threadsCount = threadsCount;
        }

        public int Evaluate(IEnumerable<int> values)
        {
            var partSize = values.Count() / _threadsCount;
            var orderedValues = values.OrderBy(x => x);

            var handles = new List<WaitHandle>();
            for (var i = 0; i < _threadsCount; i++)
            {
                var handle = new AutoResetEvent(false);
                handles.Add(handle);

                ThreadPool.QueueUserWorkItem(PartSum, new StateWrapper
                {
                    WaitHandle = handle,
                    Values = orderedValues.Skip(i * partSize).Take(partSize)
                });
            }

            WaitHandle.WaitAll(handles.ToArray());
            return _results.Sum();
        }

        private void PartSum(object stateRaw)
        {
            if (stateRaw is not StateWrapper state)
            {
                throw new ArgumentException();
            }

            var result = 0;

            foreach (var value in state.Values)
            {
                result += value;
            }

            _results.Add(result);
            state.WaitHandle.Set();
        }

        internal struct StateWrapper
        {
            public AutoResetEvent WaitHandle { get; set; }
            public IEnumerable<int> Values { get; set; }
        }
    }
}