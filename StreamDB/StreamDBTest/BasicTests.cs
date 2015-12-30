using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StreamDBTest.TestDatabases;
using System.Collections.Generic;
using System.Linq;

namespace StreamDBTest
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        public void CanSaveEmptyDB()
        {
            var db = new CustomerDatabase();
            db.SaveChanges();
        }
        [TestMethod]
        public void CanAddItem()
        {
            var db = new CustomerDatabase();
            db.People.Add(new Person() { FirstName = "Bob", LastName = "Smith" });
            Assert.AreEqual(1, db.People.Count());
            Assert.AreEqual(1, db.People.Single().RowVersion);
            Assert.AreNotEqual(Guid.Empty, db.People.Single().RowGuid);
            db.SaveChanges();
            Assert.AreEqual(1, db.People.Count());
        }
        [TestMethod]
        public void DoesWriteSomethingToStream()
        {
            var db = new CustomerDatabase();
            db.People.Add(new Person() { FirstName = "Bob", LastName = "Smith" });
            Assert.AreNotEqual(0, (db.Connection.Stream as System.IO.MemoryStream).Length);
        }
    }
}
