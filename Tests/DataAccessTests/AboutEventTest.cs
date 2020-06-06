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
    class AboutEventTest
    {
        List<AboutEvent> Items;

        [SetUp]
        public void SetUp()
        {
            Items = new List<AboutEvent>();
            Items.Add(new AboutEvent()
            {
                Description = "description"
            });
        }

        [Test]
        public void TestParameters()
        {
            Assert.AreEqual("description", Convert.ToString(Items[0].Description));
        }
    }
}
