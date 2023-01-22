using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportSections.Models
{
    public class Faculty
    {
        public int FacultyId { get; set; }
        [Display(Name = "Faculty short name")]
        [Required(ErrorMessage = "Empty field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string ShortName { get; set; }
        [Display(Name = "Faculty full name")]
        [Required(ErrorMessage = "Empty field")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string FullName { get; set; }
        public University University { get; set; }
        public int UniversityId { get; set; }
        public List<Departament> Departaments { get; set; }
    }
}
