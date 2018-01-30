using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bangazon_inc.Models
{
    // A Class that structures an Order as JSON
    // Returns JSON in an easily readable format that can be added to the ICollection "Orders" on the CustomerWithOrderJSON Class
    // Contains OrderID, CustomerID, PaymentTypeID, DateCreated
    public class OrderOnCustomerJSON
    {
        public int OrderId {get; set;}
        public int CustomerId {get; set;}
        public int PaymentTypeId {get; set;}
        public string OrderDate {get; set;}
    }
}