using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportSections.Models
{
    public class University
    {
        public int UniversityId { get; set; }
        [Display(Name = "University short name")]
        [Required(ErrorMessage = "Empty field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string ShortName { get; set; }
        [Display(Name = "University full name")]
        [Required(ErrorMessage = "Empty field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Empty field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string Address { get; set; }
        public List<Faculty> Faculties { get; set; }
    }
}
