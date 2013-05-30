using System;
using EmptyRuleValidator.Abstraction;
using EmptyRuleValidator.Data.Fields;
using EmptyRuleValidator.Data.Items;
using Sitecore.Data.Validators;

namespace EmptyRuleValidator.Data.Validator
{
    public abstract class TestableValidator : StandardValidator
    {
      
        public new Func<IItem> GetItem;
        public new Func<IField> GetField ;
        

        protected TestableValidator()
        {
            GetItem = () => new ItemWrapper(base.GetItem());
            GetField= () => new FieldWrapper(base.GetField());
        }

        
        public ValidatorResult TestEvaluate()
        {
            return Evaluate();
        }
    }
}