using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bangazon_inc.Data;

namespace bangazon_inc.Models
{
  public class Training
  {
    [Key]
    public int TrainingId { get; set; }

    [Required]
    public string TrainingName { get; set; }
[Required]
    public string StartDate { get; set;}
[Required]
    public string EndDate { get; set; }
[Required]
    public int MaxAttendees { get; set;}
  }
}