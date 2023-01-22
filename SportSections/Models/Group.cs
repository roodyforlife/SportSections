using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportSections.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        [Display(Name = "Group name")]
        [Required(ErrorMessage = "Empty field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string GroupName { get; set; }
        [Display(Name = "Create date")]
        [Required(ErrorMessage = "Empty field")]
        public DateTime CreateDate { get; set; }
        public Departament Departament { get; set; }
        public int DepartamentId { get; set; }
        public List<Student> Students { get; set; }
    }
}
