using EmptyRuleValidator.Abstraction;
using EmptyRuleValidator.Data.Database;
using EmptyRuleValidator.Data.Fields;
using Sitecore.Data.Validators;
using EmptyRuleValidator.Extensions;

namespace EmptyRuleValidator.Data.Validator
{
    public class EmptyRuleValidator : TestableValidator
    {
        private readonly IItemRepository _itemRepository;
        private readonly IRuleFieldParser _ruleFieldParser;

        public EmptyRuleValidator(IItem item, IField field, IItemRepository itemRepository, IRuleFieldParser ruleFieldParser):base(item,field)
        {
            _itemRepository = itemRepository;
            _ruleFieldParser = ruleFieldParser;
        }

        public EmptyRuleValidator()
        {
            _itemRepository = new ItemRepository(Item.Database);
            _ruleFieldParser = new RuleFieldParser();
        }

        protected override ValidatorResult Evaluate()
        {
            return ValidateRuleField(Field);
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
            var elementTextField = ElementTextField.Parse(_itemRepository.Get(element.Guid)["text"]);

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