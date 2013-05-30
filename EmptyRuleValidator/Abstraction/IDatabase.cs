using Sitecore.Data;

namespace EmptyRuleValidator.Abstraction
{
    public interface IDatabase
    {
        IItem GetItem(ID id);
    }
}
