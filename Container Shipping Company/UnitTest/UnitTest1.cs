using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Container_Shipping_Company;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDatabaseGetBestemmingen()
        {
            DatabaseManager database = new DatabaseManager();
            Assert.IsNotNull(database.GetBestemmingen().Find(x => x.Naam == "Port of Antwerp").Naam);
            
        }

        [TestMethod]
        public void TestDatabaseAddContainer()
        {
            DatabaseManager database = new DatabaseManager();
            Assert.IsTrue(database.AddContainer(new Container(0, "ITC", new Bestemming("Toronto", "Canada"), 230054, ContainerType.C, false)));
        }
    }
}
