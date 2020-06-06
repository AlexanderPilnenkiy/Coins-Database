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
    class EventsTest
    {
        List<Events> Items;

        [SetUp]
        public void SetUp()
        {
            Items = new List<Events>();
            Items.Add(new Events()
            {
                Caption = "aa",
                Type = "bb",
                Date = "12.12.2020",
                Place = "cc"
            });
        }

        [Test]
        public void TestParameters()
        {
            Assert.AreEqual("aa", Convert.ToString(Items[0].Caption));
            Assert.AreEqual("bb", Convert.ToString(Items[0].Type));
            Assert.AreEqual("12.12.2020", Convert.ToString(Items[0].Date));
            Assert.AreEqual("cc", Convert.ToString(Items[0].Place));
        }
    }
}
