using EmptyRuleValidator.Abstraction;
using Sitecore.Data.Fields;

namespace EmptyRuleValidator.Data.Fields
{
    public class FieldWrapper : CustomField, IField
    {       

        public FieldWrapper(Field innerField) : base(innerField)
        {
        }

        public FieldWrapper(Field innerField, string runtimeValue) : base(innerField, runtimeValue)
        {
        }

       
    }
}