using Coins_Database.Actions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins_Database.Tests
{
    [TestFixture]
    class ConnectionTest
    {
        [SetUp]
        public void SetUp()
        {
            Connection.Connect("olga_todorova", "coins_admin");
        }

        [TestCase("postgres")]
        [Test]
        public void TestParameters(string Database)
        {
            Assert.AreEqual(Database, Connection.Established.Database);
        }

        [TestCase(Session.ACCESS.Superadmin)]
        [Test]
        public void TestAccessType(Session.ACCESS _ACCESS)
        {
            Assert.AreEqual(_ACCESS, Session.Access);
        }
    }
}
