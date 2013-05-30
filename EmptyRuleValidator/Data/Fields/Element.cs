using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace EmptyRuleValidator.Data.Fields
{
    public class Element
    {
        public Guid Guid { get; private set; }
        public IEnumerable<string> Attributes { get; private set; }

        public static Element Parse(XElement element)
        {
            return new Element()
                {
                    Attributes = element.Attributes()
                        .Select(attribute => attribute.Name.LocalName)
                        .Where(st => !st.Equals("uid", StringComparison.InvariantCultureIgnoreCase) && !st.Equals("id", StringComparison.InvariantCultureIgnoreCase)),
                    Guid = new Guid(element.Attribute("id").Value)
                };
        }



        
    }
}