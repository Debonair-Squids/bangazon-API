using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bangazon_inc.Models
{
    // A Class that structures products as JSON
    // Contains Name, Price, Quantity, and the ProductID
    public class ProductOnOrderJSON
    {
        [Required]
        public int ProductId {get; set;}

        [Required]
        public string Name {get; set;}

        [Required]
        public double Price {get; set;}

        [Required]
        public int Quantity {get; set;}

    }

}