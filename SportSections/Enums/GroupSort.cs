using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportSections.Enums
{
    public enum GroupSort
    {
        [Display(Name = "Sort by name A-Z")]
        NameAsc,
        [Display(Name = "Sort by name Z-A")]
        NameDesc,
        [Display(Name = "New first")]
        DateAsc,
        [Display(Name = "Old first")]
        DateDesc
    }
}
