using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRuleValidator.Data.Fields;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    class ElementTextFieldTest
    {
        [Test]
        public void Macros_Count_Is_2()
        {
            //arrange
            const string textfield = "where the current database name [operatorid,StringOperator,,compares to] [value,,,value]";

            //act
            var elementTextField = ElementTextField.Parse(textfield);

            //assert

            Assert.That(elementTextField.Macros.Count(),Is.EqualTo(2));
        }

        [Test]
        public void Macros_Has_operatorid()
        {
            //arrange
            const string textfield = "where the current database name [operatorid,StringOperator,,compares to] [value,,,value]";

            //act
            var elementTextField = ElementTextField.Parse(textfield);

            //assert

            Assert.That(elementTextField.Macros.Any(s => s=="operatorid"), Is.True);
        }

        [Test]
        public void Macros_Has_value()
        {
            //arrange
            const string textfield = "where the current database name [operatorid,StringOperator,,compares to] [value,,,value]";

            //act
            var elementTextField = ElementTextField.Parse(textfield);

            //assert

            Assert.That(elementTextField.Macros.Any(s => s == "value"), Is.True);
        }

        [Test]
        public void Macros_Has_No_Macros()
        {
            //arrange
            const string textfield = "where the current database name ";

            //act
            var elementTextField = ElementTextField.Parse(textfield);

            //assert

            Assert.That(elementTextField.Macros.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Macros_Supports_Nulls()
        {
            //arrange
            const string textfield = null;

            //act
            var elementTextField = ElementTextField.Parse(textfield);

            //assert

            Assert.That(elementTextField.Macros.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Macros_Supports_Empty()
        {
            //arrange
            const string textfield = "";

            //act
            var elementTextField = ElementTextField.Parse(textfield);

            //assert

            Assert.That(elementTextField.Macros.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Macros_Supports_Strings_With_Curly_Brackets()
        {
            //arrange
            const string textfield =
                @"Add message to log file [level,Tree,root={0F40AF58-9F5F-4F1B-B11B-639E87AA86AF}&resulttype=name&setRootAsSearchRoot=true,{level}]  : [text,,,{text}]";

            //act
            var elementTextField = ElementTextField.Parse(textfield);

            //assert

            Assert.That(elementTextField.Macros.Count(), Is.EqualTo(2));
            Assert.That(elementTextField.Macros.Contains("level"),Is.True);
            Assert.That(elementTextField.Macros.Contains("text"), Is.True);
        }
       
    }
}
