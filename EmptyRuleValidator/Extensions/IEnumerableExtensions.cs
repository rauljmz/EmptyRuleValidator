using System.Collections.Generic;
using System.Linq;

namespace EmptyRuleValidator.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool ContainsSameElements<T>(this IEnumerable<T> enum1, IEnumerable<T> enum2)
        {
            return enum1.Count() == enum2.Count() && !enum1.Except(enum2).Any();
        }
    }
}