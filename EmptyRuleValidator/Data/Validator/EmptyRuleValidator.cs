using System;
using EmptyRuleValidator.Abstraction;
using EmptyRuleValidator.Data.Database;
using EmptyRuleValidator.Data.Fields;
using Sitecore.Data.Validators;
using EmptyRuleValidator.Extensions;

namespace EmptyRuleValidator.Data.Validator
{
    public class EmptyRuleValidator : TestableValidator
    {
        
        private IRuleFieldParser _ruleFieldParser;
        private IItemRepository _itemRepository;

        public IItemRepository ItemRepository
        {
            get { return _itemRepository ?? (_itemRepository = new ItemRepository(GetItem().Database)); }
            set { _itemRepository = value; }
        }

        public IRuleFieldParser RuleFieldParser
        {
            get { return _ruleFieldParser ?? (_ruleFieldParser = new RuleFieldParser()); }
            set { _ruleFieldParser = value; }
        }
        
        protected override ValidatorResult Evaluate()
        {
            return ValidateRuleField(GetField());
        }

        private ValidatorResult ValidateRuleField(IField field)
        {            
            var ruleField = _ruleFieldParser.Parse(field.Value);

            foreach (var element in ruleField.Elements)
            {
                if (!ContainsAllMacrosFromElementDefinition(element)) return GetMaxValidatorResult();
            }

            return ValidatorResult.Valid;
        }

        private bool ContainsAllMacrosFromElementDefinition(Element element)
        {            
            var elementTextField = ElementTextField.Parse(ItemRepository.Get(element.Guid)["text"]);

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