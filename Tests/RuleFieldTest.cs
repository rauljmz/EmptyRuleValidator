using System.Linq;
using EmptyRuleValidator.Data.Fields;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    class RuleFieldTest
    {
        //[SetUp]
        //public void SetUp()
        //{
            
        //}

        [Test]
        public void GetElements_Returns_3_Elements()
        {
            //arrange
            var field =  RuleField.Parse("<ruleset><rule uid=\"{D2EA389E-C63F-4606-B8DC-01F2CC03C895}\"><conditions><condition id=\"{410EE5F7-07ED-4F5C-A44D-5DF023072DD8}\" uid=\"CFBA0B43F25E4445889DDAA8921625D4\" operatorid=\"{066602E2-ED1D-44C2-A698-7ED27FD3A2CC}\" TrafficType=\"1\" /></conditions><actions><action id=\"{4D151B8B-BD5F-4479-A35F-EE740F6387E8}\" uid=\"AFB15BD3D83D4AECBFC6007D0C18A06C\" level=\"Info\" text=\"234\" /><action id=\"{94C5C335-0902-4B45-B528-11B220005DD7}\" uid=\"CAA1A2A48D434D7AB3C11C118A9B6D7D\" /></actions></rule></ruleset>");

            //act
            var elements = field.Elements;
            //assert
            Assert.That(elements.Count(),Is.EqualTo(3));
        }

        

        //[TearDown]
        //public void TearDown()
        //{
            
        //}
    }
}
