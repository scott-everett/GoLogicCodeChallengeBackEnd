using MyAddressBookAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAddressBookAPI.Tests
{
    /// <summary>
    /// Test data.
    /// </summary>
    public static class TestData
    {
        public static IQueryable<Contact> Contacts
        {
            get
            {
                int contactId = 1;
                int addressId = 1;
                int phoneDetailId = 1;

                var contacts = new List<Contact>();
                contacts.Add(new Contact
                {
                    ContactId = contactId++,
                    FirstName = "Ben",
                    Surname = "Dover",
                    EmailAddress = "ben.dover@gmail.com",

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            AddressId = addressId++,
                            Description = "Home",
                            AddressLine1 = "123 Blue Court",
                            Locality = "Blue River",
                            PostCode = "0981",
                            State = "Kingsland"
                        },
                        new Address
                        {
                            AddressId = addressId++,
                            Description = "Holiday Unit",
                            AddressLine1 = "Unit 10 Happy Waves",
                            AddressLine2 = "456 Beach Parade",
                            Locality = "Happy Beach",
                            PostCode = "0500",
                            State = "Kingsland"
                        }
                    },
                    PhoneDetails = new List<PhoneDetail>
                    {
                        new PhoneDetail
                        {
                            PhoneDetailId = phoneDetailId++,
                            Description = "Mobile",
                            PhoneNumber = "0456789012"
                        },
                        new PhoneDetail
                        {
                            PhoneDetailId = phoneDetailId++,
                            Description = "Landline",
                            PhoneNumber = "0755555555"
                        }
                    }
                });

                contacts.Add(new Contact
                {
                    ContactId = contactId++,
                    FirstName = "Isabelle",
                    Surname = "Ringing",
                    Title = "DevOps Expert",
                    EmailAddress = "isabelle.ringing@yahoo.com",
                    PhoneDetails = new List<PhoneDetail>
                    {
                        new PhoneDetail
                        {
                            PhoneDetailId = phoneDetailId++,
                            Description = "Mobile",
                            PhoneNumber = "0499999999"
                        },
                    }
                });

                return contacts.AsQueryable();
            }
        }
    }
}
