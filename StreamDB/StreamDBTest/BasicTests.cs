﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StreamDBTest.TestDatabases;
using System.Collections.Generic;
using System.Linq;
using StreamDB;

namespace StreamDBTest
{
    [TestClass]
    public class BasicTests
    {
        Person TestPerson => new Person() { FirstName = "Bob", LastName = "Smith" };
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
            db.People.Add(TestPerson);
            Assert.AreEqual(1, db.People.Count());
            Assert.AreEqual(1, db.People.Single().RowVersion);
            Assert.AreNotEqual(Guid.Empty, db.People.Single().RowGuid);
            db.SaveChanges();
            Assert.AreEqual(1, db.People.Count());
            Assert.AreEqual(TestPerson.FirstName, db.People.Single().FirstName);
        }
        [TestMethod]
        public void DoesWriteSomethingToStream()
        {
            var db = new CustomerDatabase();
            db.People.Add(TestPerson);
            Assert.AreNotEqual(0, (db.TargetConnection.Stream as System.IO.MemoryStream).Length);
        }
        [TestMethod]
        public void SyncronizesStreams()
        {
            var file = "test.db";
            var dbSource = new CustomerDatabase(new FileStreamDatabaseConnection(file));
            var dbTarget = new CustomerDatabase(null, new FileStreamDatabaseConnection(file));
            //hook up dbTarget to dbSource
            Assert.IsTrue(dbSource.IsSource);
            Assert.IsTrue(dbTarget.IsSink);


            dbSource.People.Add(TestPerson);
            Assert.AreEqual(1, dbSource.People.Count());
            Assert.AreEqual(1, dbTarget.People.Count());
        }
    }
}
