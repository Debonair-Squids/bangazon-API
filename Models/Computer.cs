using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models {

    public class Computer {

        [Key]
        public int ComputerId {get; set;}

        [Required]
        public DateTime DatePurchased {get; set;}

        [Required]
        public DateTime DateDecommissioned {get; set;}

        [Required]
        public Boolean ActiveStatus {get; set;}
    }

}