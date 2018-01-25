using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bangazon_inc.Data;

namespace bangazon_inc.Models
{
  public class Employee
  {
    [Key]
    public int EmployeeId { get; set; }

    [Required]
    public string Name { get; set; }
    [Required]

    // department id foreign key
    public int DepartmentId { get; set;}
    [Required]
    public Department Department { get; set; }
    [Required]
    public bool Supervisor { get; set;}
    [Required]
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

  }
}