namespace EmptyRuleValidator.Data.Fields
{
    public interface IRuleFieldParser
    {
        RuleField Parse(string value);
    }
}