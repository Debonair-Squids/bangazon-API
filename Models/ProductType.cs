using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
  public class ProductType
  {
    [Key]
    public int CategoryId { get; set; }

    [Required]
    public string CategoryName { get; set; }

  }
}