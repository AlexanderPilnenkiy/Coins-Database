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
    class CoinsListTest
    {
        List<CoinsList> Items;

        [SetUp]
        public void SetUp()
        {
            Items = new List<CoinsList>();
            Items.Add(new CoinsList()
            {
                IDCoin = 45,
                IDEvent = 54,
                Date = "12.12.2020",
                Description = "aa",
                Party = "bb",
                Place = "cc",
                Type = "dd"
            });
        }

        [Test]
        public void TestParameters()
        {
            Assert.AreEqual(45, Convert.ToInt32(Items[0].IDCoin));
            Assert.AreEqual(54, Convert.ToInt32(Items[0].IDEvent));
            Assert.AreEqual("12.12.2020", Convert.ToString(Items[0].Date));
            Assert.AreEqual("aa", Convert.ToString(Items[0].Description));
            Assert.AreEqual("bb", Convert.ToString(Items[0].Party));
            Assert.AreEqual("cc", Convert.ToString(Items[0].Place));
            Assert.AreEqual("dd", Convert.ToString(Items[0].Type));
        }
    }
}
