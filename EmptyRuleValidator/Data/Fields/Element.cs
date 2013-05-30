using System;
using System.Collections.Generic;

namespace EmptyRuleValidator.Data.Fields
{
    public class Element
    {
        public Element(Guid guid, IEnumerable<string> attributes)
        {
            Guid = guid;
            Attributes = attributes;
        }

        public Guid Guid { get; private set; }
        public IEnumerable<string> Attributes { get; private set; }
    }
}