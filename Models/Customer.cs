using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bangazon_inc.Data;

namespace bangazon_inc.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        public string DateCreated { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string LastActive { get; set; }
        [Required]
        public Boolean ActiveStatus { get; set; }

    }
}