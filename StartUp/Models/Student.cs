using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartUp.Models
{
    public class Student
    {   
        [Key]
        public int StudentId { get; set; }
        [Required]
        public string StudentName { get; set; }
        public int? StudentAge { get; set; }
    }
}
