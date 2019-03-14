using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyAddressBookAPI.Models
{
    /// <summary>
    /// Represents each address.
    /// </summary>
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [ForeignKey("Contact")]
        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
        [MaxLength(200)]
        public string AddressLine1 { get; set; }
        [MaxLength(200)]
        public string AddressLine2 { get; set; }
        [MaxLength(200)]
        public string Locality { get; set; }
        [MaxLength(20)]
        public string PostCode { get; set; }
        [MaxLength(100)]
        public string State { get; set; }
    }
}