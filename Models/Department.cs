using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bangazon_inc.Data;

namespace bangazon_inc.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public int SupervisorId { get; set; }
        public Employee Employee { get; set; }
        [Required]
        public double Budget { get; set; }
    }
}