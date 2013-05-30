using System;
using EmptyRuleValidator.Abstraction;
using EmptyRuleValidator.Data.Fields;
using Moq;
using NUnit.Framework;
using Sitecore.Data.Validators;

namespace Tests
{
    [TestFixture]
    class EmptyRuleValidatorTest
    {
        private EmptyRuleValidator.Data.Validator.EmptyRuleValidator _emptyRuleValidator;
        private Mock<IField> _mockField;
        private Mock<IItemRepository> _mockRepos;
        private Mock<IRuleFieldParser> _ruleFieldParser;

        [SetUp]
        public void SetUp()
        {
            _mockRepos = new Mock<IItemRepository>();
                                    
            _mockField = new Mock<IField>();

            _ruleFieldParser = new Mock<IRuleFieldParser>();
            _ruleFieldParser.Setup(r => r.Parse(It.IsAny<string>())).Returns(new RuleField());

            _emptyRuleValidator = new EmptyRuleValidator.Data.Validator.EmptyRuleValidator
                (                    
                    null,
                    _mockField.Object,
                    _mockRepos.Object,
                    _ruleFieldParser.Object
                );
        }
        

        [Test]
        public void Evaluate_Is_Valid_for_Empty_RuleField()
        {
            //arrange            

            //act
            var result = _emptyRuleValidator.TestEvaluate();

            //assert
            Assert.That(result, Is.EqualTo(ValidatorResult.Valid));
        }

        [Test]
        public void Evaluate_Returns_Error_Missing_Macro()
        {
            //arrange
            var ruleField = new RuleField(
                new Element[]
                    {
                        new Element(new Guid("{94C5C335-0902-4B45-B528-11B220005DD7}"),new string[]{} ), 
                    }
                );
            _ruleFieldParser.Setup(r => r.Parse(It.IsAny<string>())).Returns(ruleField);            
            _mockField.SetupProperty(f => f.Value, "non empty");

            var item = new Mock<IItem>();
            item.Setup(i => i["text"]).Returns("run [scriptid,Script,,specific] script");

            _mockRepos.Setup(d => d.Get(new Guid("94C5C335-0902-4B45-B528-11B220005DD7"))).Returns(item.Object);

            //act
            var result = _emptyRuleValidator.TestEvaluate();

            //assert
            Assert.That(result, Is.EqualTo(_emptyRuleValidator.MaxValidatorResult));
        }

        [Test]
        public void Evaluate_Returns_OK()
        {
            //arrange
            var ruleField = new RuleField(new Element[]
                {
                    new Element(new Guid("{94C5C335-0902-4B45-B528-11B220005DD7}"), new string[] {"scriptid"}),
                });
            
            _mockField.SetupProperty(f => f.Value, "non empty");
            _ruleFieldParser.Setup(r => r.Parse(It.IsAny<string>())).Returns(ruleField);

            var item = new Mock<IItem>();
            item.Setup(i => i["text"]).Returns("run [scriptid,Script,,specific] script");

            _mockRepos.Setup(d => d.Get(new Guid("94C5C335-0902-4B45-B528-11B220005DD7"))).Returns(item.Object);


            //act
            var result = _emptyRuleValidator.TestEvaluate();

            //assert
            Assert.That(result, Is.EqualTo(ValidatorResult.Valid));
        }
    }
}
