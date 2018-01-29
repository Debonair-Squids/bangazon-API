using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bangazon_inc.Data;

namespace bangazon_inc.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public string OrderDate { get; set; }

        [Required]

        public Boolean CompleteStatus { get; set; }

        [Required]

        public int CustomerPaymentId { get; set; }
        public CustomerPayment CustomerPayment { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public virtual ICollection<OrderProduct> ProductOrders {get; set;}

    }
}