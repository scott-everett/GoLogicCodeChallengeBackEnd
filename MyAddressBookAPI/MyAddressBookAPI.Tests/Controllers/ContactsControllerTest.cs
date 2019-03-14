using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyAddressBookAPI.Controllers;
using MyAddressBookAPI.Models;
using MyAddressBookAPI.Tests.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace MyAddressBookAPI.Tests.Controllers
{
    /// <summary>
    /// Test suite for the ContactsController.
    /// Incomplete.
    /// </summary>
    [TestClass]
    public class ContactsControllerTest
    {
        [TestMethod]
        public void GetContacts()
        {
            // Arrange
            FakeDbContext db = new FakeDbContext();
            db.AddSet(TestData.Contacts);
            var controller = new ContactsController(db);

            // Act
            var result = controller.GetContacts();

            // Assert
            Assert.IsNotNull(result, "Null result received!!");
            Assert.AreEqual(TestData.Contacts.ToList().Count, result.Count(),
                "GetContacts record count test failed!!");
        }

        [TestMethod]
        public async Task GetContact_ExistingId()
        {
            // Arrange
            FakeDbContext db = new FakeDbContext();
            db.AddSet(TestData.Contacts);
            var controller = new ContactsController(db);

            // Act
            var result = await controller.GetContact(2) as OkNegotiatedContentResult<Contact>;

            // Assert
            Assert.IsNotNull(result, "Expected contact not found!!");
            Assert.AreEqual(TestData.Contacts.Where(c => c.ContactId == 2)
                .Select(c => c.EmailAddress).FirstOrDefault(), result.Content.EmailAddress,
                "GetContact email address comparison failed!!");
        }

        [TestMethod]
        public async Task GetContact_NonExistingId()
        {
            // Arrange
            FakeDbContext db = new FakeDbContext();
            db.AddSet(TestData.Contacts);
            var controller = new ContactsController(db);

            // Act
            var result = await controller.GetContact(
                TestData.Contacts.Select(c => c.ContactId)
                .DefaultIfEmpty(0).Max() + 1) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result, "NotFoundResult expected but not found!!");
        }

        [TestMethod]
        public async Task PostContact_Success()
        {
            // Arrange
            FakeDbContext db = new FakeDbContext();
            var controller = new ContactsController(db);

            // Act
            var itemToInsert = TestData.Contacts.Where(c => c.ContactId == 1)
                .FirstOrDefault();
            var result = await controller.PostContact(itemToInsert)
                 as CreatedAtRouteNegotiatedContentResult<Contact>;
            var retrievedResult = db.Query<Contact>().Where(c => c.ContactId == 1)
                .FirstOrDefault();

            // Assert
            Assert.IsNotNull(itemToInsert);
            Assert.IsNotNull(result);
            Assert.AreEqual(itemToInsert.EmailAddress, retrievedResult.EmailAddress,
                  "PostContact email address comparison failed!!");
        }
    }
}
