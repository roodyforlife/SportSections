using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportSections.Models
{
    public class Trainer
    {
        public int TrainerId { get; set; }
        [Display(Name = "Trainer name")]
        [Required(ErrorMessage = "Empty field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string Name { get; set; }
        [Display(Name = "Trainer surname")]
        [Required(ErrorMessage = "Empty field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string Surname { get; set; }
        [Display(Name = "Trainer patronymic")]
        [Required(ErrorMessage = "Empty field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string Patronymic { get; set; }
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
        [Display(Name = "Experience (months)")]
        [Required(ErrorMessage = "Empty field")]
        public int Experience { get; set; }
        public Section Section { get; set; }
        public int SectionId { get; set; }
    }
}
