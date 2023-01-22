using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportSections.Models
{
    public class Departament
    {
        public int DepartamentId { get; set; }
        [Display(Name = "Departament short name")]
        [Required(ErrorMessage = "Empty field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string ShortName { get; set; }
        [Display(Name = "Departament full name")]
        [Required(ErrorMessage = "Empty field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string FullName { get; set; }
        public Faculty Faculty { get; set; }
        public int FacultyId { get; set; }
        public List<Group> Groups { get; set; }
    }
}
