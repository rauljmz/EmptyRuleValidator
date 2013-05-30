using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmptyRuleValidator.Abstraction;
using EmptyRuleValidator.Data.Fields;
using EmptyRuleValidator.Data.Items;
using Sitecore.Data.Validators;

namespace EmptyRuleValidator.Data.Validator
{
    public abstract class TestableValidator : StandardValidator
    {
        public IItem Item { get; set; }

        public IField Field { get; set; }

        protected TestableValidator()
        {
            Item = new ItemWrapper(base.GetItem());
            Field = new FieldWrapper(base.GetField());
        }


        protected TestableValidator(IItem item, IField field)
        {
            Item = item;
            Field = field;
        }

        public ValidatorResult TestEvaluate()
        {
            return Evaluate();
        }
    }
}