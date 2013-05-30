using System;

namespace EmptyRuleValidator.Abstraction
{
    public interface IRepository<out T>
    {
        T Get(Guid id);
    }
}