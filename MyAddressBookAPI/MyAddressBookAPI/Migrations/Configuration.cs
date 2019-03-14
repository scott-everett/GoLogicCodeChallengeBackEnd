namespace MyAddressBookAPI.Migrations
{
    using MyAddressBookAPI.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyAddressBookAPI.Models.MyAddressBookDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MyAddressBookAPI.Models.MyAddressBookDBContext context)
        {
            // Data to seed the database
            context.Contacts.AddOrUpdate<Contact>(new Contact
            {
                FirstName = "Amy",
                Surname = "Stake",
                Title = "Business Analyst",
                EmailAddress = "a.stake@ibm.com",

                Addresses = new List<Address>
                {
                    new Address
                    {
                        Description = "Home",
                        AddressLine1 = "25 Red Avenue",
                        Locality = "Red Hill",
                        PostCode = "0333",
                        State = "Kingsland"
                    },
                    new Address
                    {
                        Description = "Work",
                        AddressLine1 = "Floor 5 IBM Building",
                        AddressLine2 = "165 Yellow Street",
                        Locality = "Yellow Town",
                        PostCode = "0100",
                        State = "Kingsland"
                    }
                },

                PhoneDetails = new List<PhoneDetail>
                {
                    new PhoneDetail
                    {
                        Description = "Mobile",
                        PhoneNumber = "0488888888"
                    },
                    new PhoneDetail
                    {
                        Description = "Landline",
                        PhoneNumber = "0788888888"
                    }
                }
            });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
