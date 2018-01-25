using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
    public class CustomerPayment
    {
        [Key]
        public int CustomerPaymentId { get; set; }

        [Required]
        public int AccountNumber { get; set; }
        [Required]
        public int PaymentTypeID { get; set; }
        public PaymentType PaymentType { get; set; }
        [Required]
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
    }
}