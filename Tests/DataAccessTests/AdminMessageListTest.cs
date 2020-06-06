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
    class AdminMessageListTest
    {
        List<AdminMessageList> Items;

        [SetUp]
        public void SetUp()
        {
            Items = new List<AdminMessageList>();
            Items.Add(new AdminMessageList()
            {
                ID = 1,
                Teacher = "Петров",
                Event = "Посещение дельфинария",
                Date = "21.12.2019",
                Status = "Отклонено"
            });
        }

        [Test]
        public void TestParameters()
        {
            Assert.AreEqual(1, Convert.ToInt32(Items[0].ID));
            Assert.AreEqual("Петров", Convert.ToString(Items[0].Teacher));
            Assert.AreEqual("Посещение дельфинария", Convert.ToString(Items[0].Event));
            Assert.AreEqual("21.12.2019", Convert.ToString(Items[0].Date));
            Assert.AreEqual("Отклонено", Convert.ToString(Items[0].Status));
        }
    }
}
