using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyAddressBookAPI.Models
{
    /// <summary>
    /// Represents each phone number.
    /// </summary>
    public class PhoneDetail
    {
        [Key]
        public int PhoneDetailId { get; set; }

        [ForeignKey("Contact")]
        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
    }
}