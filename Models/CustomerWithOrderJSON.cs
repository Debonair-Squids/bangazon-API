using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bangazon_inc.Models
{
    // A Class that structures a Customer as JSON
    // Returns JSON in an easily readable format, with completed orders attached to the customer
    // Contains CustomerID, FirstName, LastName, and an array of Orders that they have completed
    public class CustomerWithOrderJSON
    {
        public int CustomerId {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public virtual ICollection<OrderOnCustomerJSON> Orders {get; set;}

    }
}