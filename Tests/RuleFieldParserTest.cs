using System;
using System.Linq;
using EmptyRuleValidator.Data.Fields;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    class RuleFieldParserTest
    {
        //[SetUp]
        //public void SetUp()
        //{
            
        //}

        [Test]
        public void GetElements_Returns_3_Elements()
        {
            //arrange
            var ruleFieldParser = new RuleFieldParser();
            const string value = "<ruleset><rule uid=\"{D2EA389E-C63F-4606-B8DC-01F2CC03C895}\"><conditions><condition id=\"{410EE5F7-07ED-4F5C-A44D-5DF023072DD8}\" uid=\"CFBA0B43F25E4445889DDAA8921625D4\" operatorid=\"{066602E2-ED1D-44C2-A698-7ED27FD3A2CC}\" TrafficType=\"1\" /></conditions><actions><action id=\"{4D151B8B-BD5F-4479-A35F-EE740F6387E8}\" uid=\"AFB15BD3D83D4AECBFC6007D0C18A06C\" level=\"Info\" text=\"234\" /><action id=\"{94C5C335-0902-4B45-B528-11B220005DD7}\" uid=\"CAA1A2A48D434D7AB3C11C118A9B6D7D\" /></actions></rule></ruleset>";

            //act
            
            var ruleField = ruleFieldParser.Parse(value); 
            
            //assert
            Assert.That(ruleField.Elements.Count(),Is.EqualTo(3));            
        }

        [Test]
        public void GetElements_Parses_Condition()
        {
            //arrange
            var ruleFieldParser = new RuleFieldParser();
            const string value = "<ruleset><rule uid=\"{D2EA389E-C63F-4606-B8DC-01F2CC03C895}\"><conditions><condition id=\"{410EE5F7-07ED-4F5C-A44D-5DF023072DD8}\" uid=\"CFBA0B43F25E4445889DDAA8921625D4\" operatorid=\"{066602E2-ED1D-44C2-A698-7ED27FD3A2CC}\" TrafficType=\"1\" /></conditions></rule></ruleset>";

            //act

            var ruleField = ruleFieldParser.Parse(value);

            //assert
            Assert.That(ruleField.Elements.Count(), Is.EqualTo(1));
            Assert.That(ruleField.Elements.First().Guid,Is.EqualTo(new Guid("{410EE5F7-07ED-4F5C-A44D-5DF023072DD8}")));
            Assert.That(ruleField.Elements.First().Attributes,Contains.Item("operatorid"));
            Assert.That(ruleField.Elements.First().Attributes, Contains.Item("TrafficType"));
        }

        [Test]
        public void GetElements_Parses_Action()
        {
            //arrange
            var ruleFieldParser = new RuleFieldParser();
            const string value = "<ruleset><rule uid=\"{D2EA389E-C63F-4606-B8DC-01F2CC03C895}\"><actions><action id=\"{4D151B8B-BD5F-4479-A35F-EE740F6387E8}\" uid=\"AFB15BD3D83D4AECBFC6007D0C18A06C\" level=\"Info\" text=\"234\" /></actions></rule></ruleset>";

            //act

            var ruleField = ruleFieldParser.Parse(value);

            //assert
            Assert.That(ruleField.Elements.Count(), Is.EqualTo(1));
            Assert.That(ruleField.Elements.First().Guid, Is.EqualTo(new Guid("{4D151B8B-BD5F-4479-A35F-EE740F6387E8}")));
            Assert.That(ruleField.Elements.First().Attributes, Contains.Item("level"));
            Assert.That(ruleField.Elements.First().Attributes, Contains.Item("text"));
        }

        [Test]
        public void Parse_Returns_Empty_RuleField_For_Null()
        {
            //arrange
            var ruleFieldParser = new RuleFieldParser();            
            
            //act
            var ruleField = ruleFieldParser.Parse(null);
            
            //assert
            Assert.That(ruleField,Is.Not.Null);
            Assert.That(ruleField.Elements,Is.Not.Null);
        }

        [Test]
        public void Parse_Returns_Empty_RuleField_For_Empty()
        {
            //arrange
            var ruleFieldParser = new RuleFieldParser();

            //act
            var ruleField = ruleFieldParser.Parse(string.Empty);

            //assert
            Assert.That(ruleField, Is.Not.Null);
            Assert.That(ruleField.Elements, Is.Not.Null);
        }

        [Test]
        public void Parse_Returns_Empty_RuleField_For_Misformed_XML()
        {
            //arrange
            var ruleFieldParser = new RuleFieldParser();

            //act
            var ruleField = ruleFieldParser.Parse("<ruleFieldL lksdjf> <F>");

            //assert
            Assert.That(ruleField, Is.Not.Null);
            Assert.That(ruleField.Elements, Is.Not.Null);
        }
        

        //[TearDown]
        //public void TearDown()
        //{
            
        //}
    }
}
