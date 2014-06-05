using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tweakers;
using System.Data;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            DBManager db = new DBManager();
            Categorie result = db.GetCategorie(0);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.CatID);
        }
    }
}
