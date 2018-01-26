using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bangazon_inc.Data;

namespace bangazon_inc.Models {

    public class Computer {

        [Key]
        public int ComputerId {get; set;}

        [Required]
        public string DatePurchased {get; set;}

        public string DateDecommissioned {get; set;}

        [Required]
        public Boolean ActiveStatus {get; set;}
    }
}