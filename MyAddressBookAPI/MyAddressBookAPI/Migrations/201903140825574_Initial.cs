namespace MyAddressBookAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        ContactId = c.Int(nullable: false),
                        Description = c.String(maxLength: 200),
                        AddressLine1 = c.String(maxLength: 200),
                        AddressLine2 = c.String(maxLength: 200),
                        Locality = c.String(maxLength: 200),
                        PostCode = c.String(maxLength: 20),
                        State = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 200),
                        Surname = c.String(maxLength: 200),
                        Title = c.String(maxLength: 200),
                        EmailAddress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ContactId);
            
            CreateTable(
                "dbo.PhoneDetails",
                c => new
                    {
                        PhoneDetailId = c.Int(nullable: false, identity: true),
                        ContactId = c.Int(nullable: false),
                        Description = c.String(maxLength: 200),
                        PhoneNumber = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.PhoneDetailId)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ContactId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.PhoneDetails", "ContactId", "dbo.Contacts");
            DropIndex("dbo.PhoneDetails", new[] { "ContactId" });
            DropIndex("dbo.Addresses", new[] { "ContactId" });
            DropTable("dbo.PhoneDetails");
            DropTable("dbo.Contacts");
            DropTable("dbo.Addresses");
        }
    }
}
