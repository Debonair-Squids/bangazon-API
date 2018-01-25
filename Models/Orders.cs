using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bangazon_inc.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }

        [Required]

        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime OrderDate { get; set; }

        [Required]

        public Boolean CompleteStatus { get; set; }

        [Required]

        public int CustomerPaymentId { get; set; }
        public CustomerPayment CustomerPayment { get; set; }

        [Required]

        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

    }
}