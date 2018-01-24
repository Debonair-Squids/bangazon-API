using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
  public class Training
  {
    [Key]
    public int TrainingId { get; set; }

    [Required]
    public string TrainingName { get; set; }

    public DateTime StartDate { get; set;}
    public DateTime EndDate { get; set; }
    public int MaxAttendes { get; set;}
  }
}