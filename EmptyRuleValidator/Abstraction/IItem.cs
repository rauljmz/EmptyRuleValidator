using Sitecore.Data;

namespace EmptyRuleValidator.Abstraction
{
    public interface IItem
    {
        string this[string fieldName] { get; set; }
        IDatabase Database { get; set; }
        ID ID { get; set; }
    }
}
