using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using EmptyRuleValidator.Extensions;

namespace Tests
{
    [TestFixture]
    class IEnumerableExtensionsTest
    {
        [Test]
        public void Empty_Lists_Are_Equal()
        {
            //arrange
            IEnumerable<object> a = new List<object>();
            IEnumerable<object> b = new List<object>();
            //act
            var result = a.ContainsSameElements(b);
            //assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void List_Subset_Is_Not_Equal()
        {
            //arrange
            IEnumerable<object> a = new List<object> { 1, 2, 3 };
            IEnumerable<object> b = new List<object> { 1, 2, 3, 4 };
            //act
            var result = a.ContainsSameElements(b);
            //assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void List_Superset_Is_Not_Equal()
        {
            //arrange
            IEnumerable<object> a = new List<object> { 1, 2, 3, 4 };
            IEnumerable<object> b = new List<object> { 1, 2, 3 };
            //act
            var result = a.ContainsSameElements(b);
            //assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void List_Same_Elements_Different_Order_Is_Equal()
        {
            //arrange
            IEnumerable<object> a = new List<object> { 1, 2, 3, 4 };
            IEnumerable<object> b = new List<object> { 3, 2, 1, 4 };
            //act
            var result = a.ContainsSameElements(b);
            //assert
            Assert.That(result, Is.True);
        }
    }
}
