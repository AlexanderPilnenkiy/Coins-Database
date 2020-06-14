using Coins_Database.Operations;
using Coins_Database.Views;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Coins_Database.Tests.OperationsTests
{
    [TestFixture]
    class DetermineCoinTypeTest
    {
        DetermineCoinType determineCoinType;

        [SetUp]
        public void SetUp()
        {
            determineCoinType = new DetermineCoinType();
        }

        //[TestCase(0, "Арткоин")]
        //[TestCase(1, "Талант")]
        //[TestCase(2, "Соц. активность")]
        //[TestCase(3, "Интеллект")]
        //[TestCase(-1, "Ошибка")]
        //[Test]
        //public void TestParameters(int Id, string Coin)
        //{
        //    Assert.AreEqual(Id, determineCoinType.CoinType(Coin));
        //}
    }
}
