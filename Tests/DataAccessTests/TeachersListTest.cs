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
    class TeachersListTest
    {
        List<TeachersList> Items;

        [SetUp]
        public void SetUp()
        {
            Items = new List<TeachersList>();
            Items.Add(new TeachersList()
            {
                FIO = "FIO",
                Speciality = "Speciality",
                ID = 45
            });
        }

        [Test]
        public void TestParameters()
        {
            Assert.AreEqual(45, Convert.ToInt32(Items[0].ID));
            Assert.AreEqual("FIO", Convert.ToString(Items[0].FIO));
            Assert.AreEqual("Speciality", Convert.ToString(Items[0].Speciality));
        }
    }
}
