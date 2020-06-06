using Coins_Database.DataAccessLayer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins_Database.Tests
{
    [TestFixture]
    class AccountDataTests
    {
        List<Account> Items;

        [SetUp]
        public void SetUp()
        {
            Items = new List<Account>();
            Items.Add(new Account()
            {
                ID = 45,
                Name = "name",
                Login = "login"
            });
        }

        [Test]
        public void TestParameters()
        {
            Assert.AreEqual(45, Convert.ToInt32(Items[0].ID));
            Assert.AreEqual("name", Convert.ToString(Items[0].Name));
            Assert.AreEqual("login", Convert.ToString(Items[0].Login));
        }
    }
}
