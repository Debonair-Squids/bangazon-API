using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bangazon_inc.Models
{
    // A Class that structures orders as JSON
    // Returns JSON in an easily readable format, with products attached to the order and their quantity
    // Contains OrderID, CustomerID, PaymentTypeID, and an array of Products
    public class OrderWithProductJSON
    {
        public int OrderId {get; set;}
        public int CustomerId {get; set;}
        public int PaymentTypeId {get; set;}
        public virtual ICollection<ProductOnOrderJSON> Products {get; set;}

    }
}