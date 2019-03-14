using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyAddressBookAPI.Models;
using MyAddressBookAPI.Tests.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAddressBookAPI.Tests.FakesTests
{
    /// <summary>
    /// Tests I built to test the FakeDbContext.
    /// Incomplete.
    /// </summary>
    [TestClass]
    public class FakeDbContextTest
    {
        [TestMethod]
        public void Find_StandardPrimaryKey()
        {
            // Arrange
            FakeDbContext db = new FakeDbContext();
            db.AddSet(TestData.Contacts);

            // Act
            var contact2 = db.Find<Contact>(2);
            var contact1 = db.Find<Contact>(1);

            // Assert
            Assert.AreEqual("ben.dover@gmail.com", contact1.EmailAddress,
                "Find - Contact 1 email address check failed!!");
            Assert.AreEqual("isabelle.ringing@yahoo.com", contact2.EmailAddress,
                "Find - Contact 2 email address check failed!!");
        }

        [TestMethod]
        public async Task FindAsync_StandardPrimaryKey()
        {
            // Arrange
            FakeDbContext db = new FakeDbContext();
            db.AddSet(TestData.Contacts);

            // Act
            var taskContact2 = await db.FindAsync<Contact>(2);
            var taskContact1 = await db.FindAsync<Contact>(1);

            // Assert
            Assert.AreEqual("ben.dover@gmail.com", taskContact1.EmailAddress,
                "FindAsync - Contact 1 email address check failed!!");
            Assert.AreEqual("isabelle.ringing@yahoo.com", taskContact2.EmailAddress,
                "FindAsync - Contact 2 email address check failed!!");
        }

        [TestMethod]
        public void Add()
        {
            // Arrange
            FakeDbContext db = new FakeDbContext();
            db.AddSet(TestData.Contacts);
            int newContactId = TestData.Contacts.Select(c => c.ContactId)
                .DefaultIfEmpty(0).Max() + 1;
            var newContact = new Contact
            {
                ContactId = newContactId,
                FirstName = "Anita",
                Surname = "Bath",
                EmailAddress = "anita.bath@gmail.com"
            };

            // Act
            db.Add(newContact);
            var addedContact = db.Find<Contact>(newContactId);

            // Assert
            Assert.AreEqual("Bath", addedContact.Surname,
                "New contact surname check failed!!");
            Assert.AreEqual(1, db.Added.Count,
                "New contact count check failed!!");
        }

        [TestMethod]
        public void Update()
        {
            // Arrange
            FakeDbContext db = new FakeDbContext();
            db.AddSet(TestData.Contacts);

            // Act
            var contact2 = db.Find<Contact>(2);
            contact2.Title = "Brain Surgeon";
            db.Update<Contact>(contact2);
            var updatedContact = db.Find<Contact>(2);

            // Assert
            Assert.AreEqual("Brain Surgeon", updatedContact.Title,
                "Updated contact title check failed");
            Assert.AreEqual(1, db.Updated.Count,
                "Updated contact count check failed");
        }

        [TestMethod]
        public void Remove()
        {
            // Arrange
            FakeDbContext db = new FakeDbContext();
            db.AddSet(TestData.Contacts);

            // Act
            var contact2 = db.Find<Contact>(2);
            db.Remove<Contact>(contact2);
            var removedContact = db.Find<Contact>(2);

            // Assert
            Assert.AreEqual(null, removedContact);
            Assert.AreEqual(1, db.Removed.Count);
        }
    }
}
