using System.Collections.Generic;

namespace Otus28
{
    public class SequentalEvaluator : IEValuator
    {
        public int Evaluate(IEnumerable<int> values)
        {
            var result = 0;

            foreach (var value in values)
            {
                result += value;
            }

            return result;
        }
    }
}