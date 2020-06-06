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
    class RatingTest
    {
        List<Rating> Items;

        [SetUp]
        public void SetUp()
        {
            Items = new List<Rating>();
            Items.Add(new Rating()
            {
                ID = 45,
                FIO = "FIO",
                Coin = 12
            });
        }

        [Test]
        public void TestParameters()
        {
            Assert.AreEqual(45, Convert.ToInt32(Items[0].ID));
            Assert.AreEqual(12, Convert.ToInt32(Items[0].Coin));
            Assert.AreEqual("FIO", Convert.ToString(Items[0].FIO));
        }
    }
}
