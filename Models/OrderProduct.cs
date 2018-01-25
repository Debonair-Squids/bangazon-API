using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bangazon_inc.Data;

namespace bangazon_inc.Models
{
    public class OrderProduct {

        [Key]
        public int OrderProductId {get; set;}

        [Required]
        public int ProductId {get; set;}
        public Product Product {get; set;}

        [Required]
        public int OrderId {get; set;}
        public Orders Orders {get; set; }
    }

}