using System;
using System.Collections.Generic;
using EmptyRuleValidator.Abstraction;
using EmptyRuleValidator.Data.Validator;
using Moq;
using NUnit.Framework;
using Sitecore.Data;
using Sitecore.Data.Validators;

namespace Tests
{
    [TestFixture]
    class EmptyRuleValidatorTest
    {
        private EmptyRuleValidator.Data.Validator.EmptyRuleValidator _emptyRuleValidator;
        private Mock<IItem> _mockItem;
        private Mock<IField> _mockField;
        private Mock<IItemRepository> _mockRepos;

        private Dictionary<ID, IItem> _tempDatabase;
            
            [SetUp]
        public void SetUp()
        {
            _tempDatabase = new Dictionary<ID, IItem>();
            _mockRepos = new Mock<IItemRepository>();
                                    
            _mockField = new Mock<IField>();

            _emptyRuleValidator = new EmptyRuleValidator.Data.Validator.EmptyRuleValidator
                (                    
                    null,
                    _mockField.Object,
                    _mockRepos.Object
                );
        }

        [Test]
        public void Evaluate_Is_Valid_for_Null_Field()
        {
            //arrange
            _mockField.SetupProperty(f => f.Value, null);            

            //act
            var result = _emptyRuleValidator.TestEvaluate();
            
            //assert
            Assert.That(result,Is.EqualTo(ValidatorResult.Valid));
        }

        [Test]
        public void Evaluate_Is_Valid_for_Empty_Field()
        {
            //arrange
            _mockField.SetupProperty(f => f.Value, "");

            //act
            var result = _emptyRuleValidator.TestEvaluate();

            //assert
            Assert.That(result, Is.EqualTo(ValidatorResult.Valid));
        }

        [Test]
        public void Evaluate_Returns_Error_Missing_Macro()
        {
            //arrange
            const string ruleValueWithError = "<ruleset><rule uid=\"{D2EA389E-C63F-4606-B8DC-01F2CC03C895}\">"+
                                              "<conditions/>"+
                                              "<actions>"+
                                              "<action id=\"{94C5C335-0902-4B45-B528-11B220005DD7}\" uid=\"CAA1A2A48D434D7AB3C11C118A9B6D7D\" /></actions></rule></ruleset>";

            _mockField.SetupProperty(f => f.Value, ruleValueWithError);

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
            const string ruleValueWithError = "<ruleset><rule uid=\"{D2EA389E-C63F-4606-B8DC-01F2CC03C895}\">" +
                                              "<conditions/>" +
                                              "<actions>" +
                                              "<action id=\"{94C5C335-0902-4B45-B528-11B220005DD7}\" uid=\"CAA1A2A48D434D7AB3C11C118A9B6D7D\" scriptid=\"{3D33C178-F5A0-4132-A326-E1A06EBA459A}\"/></actions></rule></ruleset>";

            _mockField.SetupProperty(f => f.Value, ruleValueWithError);

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
