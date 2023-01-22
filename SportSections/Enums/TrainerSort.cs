using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportSections.Enums
{
    public enum TrainerSort
    {
        [Display(Name = "Sort by name")]
        NameAsc,
        [Display(Name = "Sort by surname")]
        SurnameAsc,
        [Display(Name = "Sort by patronymic")]
        PatronymicAsc,
        [Display(Name = "Sort by email A-Z")]
        EmailAsc,
        [Display(Name = "Sort by email Z-A")]
        EmailDesc,
        [Display(Name = "Sort by Experience")]
        ExperienceAsc
    }
}
