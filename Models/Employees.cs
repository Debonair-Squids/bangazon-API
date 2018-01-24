using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
  public class Employee
  {
    [Key]
    public int EmployeeId { get; set; }

    [Required]
    public string Name { get; set; }

    // department id foreign key
    public int DepartmentId { get; set;}
    public Department Department { get; set; }
    public bool Supervisor { get; set;}
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

  }
}