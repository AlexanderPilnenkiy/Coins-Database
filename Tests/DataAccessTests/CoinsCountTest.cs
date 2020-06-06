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
    class CoinsCountTest
    {
        List<CoinsCount> Items;

        [SetUp]
        public void SetUp()
        {
            Items = new List<CoinsCount>();
            Items.Add(new CoinsCount()
            {
                Artcoins = "Artcoins",
                Talents = "Talents",
                Intellect = "Intellect",
                SocActive = "SocActive"
            });
        }

        [Test]
        public void TestParameters()
        {
            Assert.AreEqual("Artcoins", Convert.ToString(Items[0].Artcoins));
            Assert.AreEqual("Talents", Convert.ToString(Items[0].Talents));
            Assert.AreEqual("Intellect", Convert.ToString(Items[0].Intellect));
            Assert.AreEqual("SocActive", Convert.ToString(Items[0].SocActive));
        }
    }
}
