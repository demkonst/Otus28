using System.Collections.Generic;

namespace Otus28
{
    public interface IEValuator
    {
        int Evaluate(IEnumerable<int> values);
    }
}