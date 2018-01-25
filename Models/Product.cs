using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
  public class Product
  {
    [Key]
    public int ProductId { get; set; }

    [Required]
    public string Name { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set;}
    public string Title { get; set; }
    public string Description { get; set; }
    // customer id foreign key
    public int CustomerId { get; set;}
    public Customer Customer { get; set; }
    // category id foreign key
    public int CategoryId { get; set;}
    public ProductType ProductType { get; set; }

  }
}