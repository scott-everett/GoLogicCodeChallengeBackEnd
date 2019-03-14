using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyAddressBookAPI.Models
{
    /// <summary>
    /// Main contact record.
    /// </summary>
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        [MaxLength(200)]
        public string FirstName { get; set; }
        [MaxLength(200)]
        public string Surname { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(100)]
        public string EmailAddress { get; set; }

        // Navigation properties
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<PhoneDetail> PhoneDetails { get; set; }
    }
}