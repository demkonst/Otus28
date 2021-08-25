using System.Collections.Generic;
using System.Linq;

namespace Otus28
{
    public class ParallelEvaluator : IEValuator
    {
        public int Evaluate(IEnumerable<int> values)
        {
            return values.AsParallel()
                .Aggregate(0, (x, y) => x + y, (x, y) => x + y, x => x);
        }
    }
}