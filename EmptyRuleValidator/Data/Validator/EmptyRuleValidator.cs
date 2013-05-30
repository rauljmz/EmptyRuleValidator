using EmptyRuleValidator.Abstraction;
using EmptyRuleValidator.Data.Fields;
using Sitecore.Data;
using Sitecore.Data.Validators;
using EmptyRuleValidator.Extensions;

namespace EmptyRuleValidator.Data.Validator
{
    public class EmptyRuleValidator : TestableValidator
    {
        public EmptyRuleValidator(IItem item, IField field):base(item,field)
        {
            
        }
        
        protected override ValidatorResult Evaluate()
        {
            var field = Field;
            if (field == null || string.IsNullOrEmpty(field.Value)) return ValidatorResult.Valid;
            
            return ValidateRuleField(field);
        }

        private ValidatorResult ValidateRuleField(IField field)
        {
            var ruleField = RuleField.Parse(field.Value);

            foreach (var element in ruleField.Elements)
            {
                if (!ContainsAllMacrosFromElementDefinition(element)) return GetMaxValidatorResult();
            }

            return ValidatorResult.Valid;
        }

        private bool ContainsAllMacrosFromElementDefinition(Element element)
        {
            var elementTextField = ElementTextField.Parse(Item.Database.GetItem(new ID(element.Guid))["text"]);

            var containsSameElements = elementTextField.Macros.ContainsSameElements(element.Attributes);
            return containsSameElements;
        }


        protected override ValidatorResult GetMaxValidatorResult()
        {
            return ValidatorResult.Error;            
        }

        public override string Name
        {
            get { return "Empty Rule Element Validator"; }
        }
    }
}