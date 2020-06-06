using Coins_Database.DataAccessLayer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins_Database.Tests.DataAccessTests
{
    [TestFixture]
    class cbEventsSortTest
    {
        List<cbEventsSort> Items;

        [SetUp]
        public void SetUp()
        {
            Items = new List<cbEventsSort>();
            Items.Add(new cbEventsSort()
            {
                Item = "item"
            });
        }

        [Test]
        public void TestParameters()
        {
            Assert.AreEqual("item", Convert.ToString(Items[0].Item));
        }
    }
}
