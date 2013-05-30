using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace EmptyRuleValidator.Data.Fields
{
    public class RuleFieldParser : IRuleFieldParser
    {
        private Element ParseElement(XElement element)
        {
            var attributes = element.Attributes()
                                    .Select(attribute => attribute.Name.LocalName)
                                    .Where(
                                        st =>
                                        !st.Equals("uid", StringComparison.InvariantCultureIgnoreCase) &&
                                        !st.Equals("id", StringComparison.InvariantCultureIgnoreCase));
            var guid = new Guid(element.Attribute("id").Value);

            return new Element(guid, attributes.ToArray());
        }

        public RuleField Parse(string value)
        {
            if (value == null) return new RuleField();
            try
            {
                var document = XDocument.Parse(value);
                var elements =
                document.Descendants().Where(element => element.Name.LocalName.Equals("condition", StringComparison.InvariantCulture) || element.Name.LocalName.Equals("action", StringComparison.InvariantCultureIgnoreCase)).Select(ParseElement).ToArray();

                return elements.Any() ? new RuleField(elements) : new RuleField();
            }
            catch (XmlException)
            {
                return new RuleField();                    
            }
        }
    }
}