using System;
using System.Linq;
using System.Xml.Linq;
using EmptyRuleValidator.Data.Fields;
using NUnit.Framework;

namespace Tests
{
     [TestFixture]
    class ElementTest
    {
        [Test]
        public void Element_Has_Element_TrafficType()
        {
            //arrange
            var xelement = XElement.Parse("<condition id=\"{410EE5F7-07ED-4F5C-A44D-5DF023072DD8}\" uid=\"CFBA0B43F25E4445889DDAA8921625D4\" operatorid=\"{066602E2-ED1D-44C2-A698-7ED27FD3A2CC}\" TrafficType=\"1\" />");

            //act
            var element = Element.Parse(xelement);

            //assert
            Assert.That(element.Attributes.FirstOrDefault(), Is.EqualTo("operatorid"));
        }

         [Test]
        public void Element_Has_One_Element()
        {
            //arrange
            var xelement = XElement.Parse("<condition id=\"{410EE5F7-07ED-4F5C-A44D-5DF023072DD8}\" uid=\"CFBA0B43F25E4445889DDAA8921625D4\" operatorid=\"{066602E2-ED1D-44C2-A698-7ED27FD3A2CC}\" TrafficType=\"1\" />");

            //act
            var element = Element.Parse(xelement);

            //assert
            Assert.That(element.Attributes.Count(), Is.EqualTo(2));
        }

         [Test]
        public void Element_Returns_Guid()
        {
            //arrange
            var xelement = XElement.Parse("<condition id=\"{410EE5F7-07ED-4F5C-A44D-5DF023072DD8}\" uid=\"CFBA0B43F25E4445889DDAA8921625D4\" operatorid=\"{066602E2-ED1D-44C2-A698-7ED27FD3A2CC}\" TrafficType=\"1\" />");

            //act
            var element = Element.Parse(xelement);

            //assert
            Assert.That(element.Guid, Is.EqualTo( new Guid("410EE5F7-07ED-4F5C-A44D-5DF023072DD8")));
        }
    }
}
