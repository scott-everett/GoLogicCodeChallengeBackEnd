GoLogic Coding Challenge - Results
==================================

NOTE - I'm only new to Angular 7 and I had a go at developing a front-end solution using this framework.  Unfortunately it was taking me too long as there are a number of required things that I am yet to learn so I am submitting a back-end solution only.  I will continue to work on an Angular 7 solution as this is what I am studying at the moment.


Problem Description
-------------------
Build a RESTFul API for a Address Book application.  Build the corresponding database and tables.  Each contact can have 0 to many corresponding addresses and phone numbers.  The solution is back-end only. 


Dev Installation Instructions
-----------------------------
* Download the files from https://github.com/scott-everett/GoLogicCodeChallengeBackEnd and copy to the desired local install location.
* Open the MyAddressBookAP\MyAddressBookAPI.sln file in Visual Studio (2017).
* Open Web.config and change the "MyAddressBook" connection string to point to the desired SQL Server instance. 
* Select Build / Rebuild Solution.
* Open the Package Manager Console (Tools / Nuget Package Manager / Package Manager Console).
* Type "Update-Database" and press enter.

You can now run the API.


Contacts API Documention
------------------------
Format: JSON

GET api/Contacts - returns a list of all contacts.

GET api/Contacts/{id} - returns the contact with the specified id.
- params: id

PUT api/Contacts/{id} - modifies the contact with the specified id.
- params: id, contact

POST api/Contacts - adds a new contact.
- params: contact

DELETE api/Contacts/{id} - deletes the contact with the specified id.
- params: id


Notes
----
* I decided to use dependency injection of implementations of the custom IDbContext interface on the API controller (ContactsController) so that a fake DbContext can be used to test using local memory rather than utilizing the database.

* The testing does not have full coverage.  I would normally test every condition path each method.  This work is just a sample.  I created unit tests for the FakeDbContext and the ContactsController.

* I do not have experience in automated intergration testing.  My experience is just with unit testing.  If I had more time I would have researched and included some of these tests.  I would test that the API successfully receives and returns JSON data.

* If I have more time I could add authentication to the solution to secure contact data.

* I added the CORS package to the project.  Currently this security is relaxed but can be set in Web.config if necessary.  Current settings...
    <httpProtocol>
      <customHeaders>
        <add name="Access-Control-Allow-Origin" value="*" />
        <add name="Access-Control-Allow-Headers" value="Content-Type" />
        <add name="Access-Control-Allow-Methods" value="GET,POST,PUT,DELETE,OPTIONS" />
        <add name="Access-Control-Allow-Credentials" value="true" />
      </customHeaders>
    </httpProtocol>


My Code
=======
MyAddressBookAPI
----------------
* Controllers\ContactsController.cs - originally scaffolded but modified to use injected IDbContext.
* Models\Address.cs
* Models\Contact.cs
* Models\IDbContext.cs
* Models\MyAddressBookDBContext.cs
* Models\PhoneDetail.cs

MyAddressBookAPI.Tests
----------------------
* Controllers\ContactsControllerTest.cs
* Fakes\FakeDbContext.cs
* FakesTests\FakeDbContextTest.cs
* TestData.cs










