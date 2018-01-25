using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bangazon_inc.Data;

namespace bangazon_inc.Models
{
    public class EmployeeTraining
    {
        [Key]
        public int EmployeeTrainingId { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public Employee Employee { get; set; }
        [Required]
        public int TrainingId { get; set; }
        [Required]
        public Training Training { get; set; }
    }
}