using Coins_Database.Views;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins_Database.Tests.InformationSystemTests
{
    [TestFixture]
    class SemestrTest
    {
        [TestCase(2, 4)]
        [TestCase(1, 10)]
        [Test]
        public void TestParameters(int Id, int Month)
        {
            Assert.AreEqual(Id, MainWindow.GetSemestr(Month));
        }
    }
}
