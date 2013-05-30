using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EmptyRuleValidator.Data.Fields
{
    public class RuleField
    {
        
        public IEnumerable<Element> Elements { get; private set; }
        
        public static RuleField Parse(string value)
        {
            var document = XDocument.Parse(value);
            return new RuleField()
                {
                    Elements = document.Descendants()
                                       .Where(
                                           element =>
                                           element.Name.LocalName.Equals("condition", StringComparison.InvariantCulture) ||
                                           element.Name.LocalName.Equals("action",
                                                                         StringComparison.InvariantCultureIgnoreCase))
                                       .Select(Element.Parse)
                };
        }
         

    }
}