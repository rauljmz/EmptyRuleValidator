using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRuleValidator.Abstraction;
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
        private  Mock<IItem> _mockItem;
        private Mock<IDatabase> _mockDatabase;
        private Mock<IField> _mockField;
        private readonly string _ruleValueWithError = "<ruleset><rule uid=\"{D2EA389E-C63F-4606-B8DC-01F2CC03C895}\">"+
            "<conditions>"+
            "<condition id=\"{410EE5F7-07ED-4F5C-A44D-5DF023072DD8}\" uid=\"CFBA0B43F25E4445889DDAA8921625D4\" operatorid=\"{066602E2-ED1D-44C2-A698-7ED27FD3A2CC}\" TrafficType=\"1\" /></conditions>"+
            "<actions>"+
            "<action id=\"{4D151B8B-BD5F-4479-A35F-EE740F6387E8}\" uid=\"AFB15BD3D83D4AECBFC6007D0C18A06C\" level=\"Info\" text=\"234\" />"+
            "<action id=\"{94C5C335-0902-4B45-B528-11B220005DD7}\" uid=\"CAA1A2A48D434D7AB3C11C118A9B6D7D\" /></actions></rule></ruleset>";
        private readonly string _ruleValueWithOutError = "<ruleset><rule uid=\"{D2EA389E-C63F-4606-B8DC-01F2CC03C895}\">" +
            "<conditions>" +
            "<condition id=\"{410EE5F7-07ED-4F5C-A44D-5DF023072DD8}\" uid=\"CFBA0B43F25E4445889DDAA8921625D4\" operatorid=\"{066602E2-ED1D-44C2-A698-7ED27FD3A2CC}\" TrafficType=\"1\" /></conditions>" +
            "<actions>" +
            "<action id=\"{4D151B8B-BD5F-4479-A35F-EE740F6387E8}\" uid=\"AFB15BD3D83D4AECBFC6007D0C18A06C\" level=\"Info\" text=\"234\" />" +
            "<action id=\"{94C5C335-0902-4B45-B528-11B220005DD7}\" uid=\"CAA1A2A48D434D7AB3C11C118A9B6D7D\" scriptid=\"{3D33C178-F5A0-4132-A326-E1A06EBA459A}\" /></actions></rule></ruleset>";

        private Dictionary<ID, IItem> _tempDatabase;
            
            [SetUp]
        public void SetUp()
        {
                _tempDatabase = new Dictionary<ID, IItem>();
            _mockItem = new Mock<IItem>();
            _mockDatabase = new Mock<IDatabase>();
            _mockField = new Mock<IField>();
            _mockItem.SetupProperty(i => i.Database,_mockDatabase.Object);
            _emptyRuleValidator = new EmptyRuleValidator.Data.Validator.EmptyRuleValidator
                (                    
                    _mockItem.Object,
                    _mockField.Object
                );
            var traffictypecondition = new Mock<IItem>();
            traffictypecondition.Setup(i => i["text"])
                                .Returns(
                                    "where the traffic type [operatorid,Operator,,compares to] [TrafficType,,,specific value]");
            traffictypecondition.SetupProperty(i => i.ID, new ID("{410EE5F7-07ED-4F5C-A44D-5DF023072DD8}"));
            AssignItemToDatabase(traffictypecondition.Object);

            var logmessagecondition = new Mock<IItem>();
            logmessagecondition.Setup(i => i["text"])
                               .Returns(
                                   "Add message to log file [level,Tree,root={0F40AF58-9F5F-4F1B-B11B-639E87AA86AF}&resulttype=name&setRootAsSearchRoot=true,{level}]  : [text,,,{text}]");
            logmessagecondition.SetupProperty(i => i.ID, new ID("{4D151B8B-BD5F-4479-A35F-EE740F6387E8}"));
            AssignItemToDatabase(logmessagecondition.Object);

            var runscriptaction = new Mock<IItem>();
            runscriptaction.Setup(i => i["text"]).Returns("run [scriptid,Script,,specific] script");            
            runscriptaction.SetupProperty(i => i.ID, new ID("{94C5C335-0902-4B45-B528-11B220005DD7}"));
            AssignItemToDatabase(runscriptaction.Object);
                
            _mockDatabase.Setup(d => d.GetItem(It.IsAny<ID>())).Returns((ID id)=> GetItemFromTempDatabase(id));


        }

        private IItem GetItemFromTempDatabase(ID id)
        {
            if (_tempDatabase.ContainsKey(id))
            {
                return _tempDatabase[id];   
            }

            var tempitem = new Mock<IItem>();
            tempitem.SetupProperty(i => i.ID, id);
            tempitem.Setup(i => i[It.IsAny<string>()]).Returns(It.IsAny<string>());
            return tempitem.Object;
        }

        private void AssignItemToDatabase(IItem item)
        {            
            _tempDatabase.Add(item.ID,item);            
        }

        [Test]
        public void Evaluate_Is_Valid_for_Null_Field()
        {
            //arrange
            _mockItem.Setup(i => i["text"]).Returns((string)null);            

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
            _mockField.SetupProperty(f => f.Value, _ruleValueWithError);            

            //act
            var result = _emptyRuleValidator.TestEvaluate();

            //assert
            Assert.That(result, Is.EqualTo(_emptyRuleValidator.MaxValidatorResult));
        }

        [Test]
        public void Evaluate_Returns_OK()
        {
            //arrange
            _mockField.SetupProperty(f => f.Value, _ruleValueWithOutError);

            //act
            var result = _emptyRuleValidator.TestEvaluate();

            //assert
            Assert.That(result, Is.EqualTo(ValidatorResult.Valid));
        }
    }
}
