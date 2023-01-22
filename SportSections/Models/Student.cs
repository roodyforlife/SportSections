using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportSections.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Display(Name = "Student name")]
        [Required(ErrorMessage = "Empty field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string StudentName { get; set; }
        [Display(Name = "Student surname")]
        [Required(ErrorMessage = "Empty field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string StudentSurname { get; set; }
        [Display(Name = "Student patronymic")]
        [Required(ErrorMessage = "Empty field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string StudentPatronymic { get; set; }
        [Required(ErrorMessage = "Empty field")]
        public DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Empty field")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Empty field")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Empty field")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string Address { get; set; }
        [Display(Name = "Date of admission")]
        [Required(ErrorMessage = "Empty field")]
        public DateTime AdmissionDate { get; set; }
        public Group Group { get; set; }
        public int GroupId { get; set; }
        public List<StudentSection> StudentSections { get; set; }
    }
}
