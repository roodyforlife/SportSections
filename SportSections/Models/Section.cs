using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportSections.Models
{
    public class Section
    {
        public int SectionId { get; set; }
        [Display(Name = "Section name")]
        [Required(ErrorMessage = "Empty field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Empty field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string Address { get; set; }
        [Display(Name = "Floor number")]
        [Required(ErrorMessage = "Empty field")]
        public int Floor { get; set; }
        [Display(Name = "Start date")]
        [Required(ErrorMessage = "Empty field")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Finish date")]
        [Required(ErrorMessage = "Empty field")]
        public DateTime FinishDate { get; set; }
        public List<StudentSection> StudentSections { get; set; }
        public List<TrainerSection> TrainerSections { get; set; }
    }
}
