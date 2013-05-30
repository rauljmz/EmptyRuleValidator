using System.Collections.Generic;

namespace EmptyRuleValidator.Data.Fields
{
    public class RuleField
    {
        public RuleField()
        {
            Elements = new Element[] {};
        }

        public RuleField(IEnumerable<Element> elements )
        {
            Elements = elements;
        }

        public IEnumerable<Element> Elements { get; private set; }                        
    }
}