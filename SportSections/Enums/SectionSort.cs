using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportSections.Enums
{
    public enum SectionSort
    {
        [Display(Name = "Sort by name A-Z")]
        NameAsc,
        [Display(Name = "Sort by name Z-A")]
        NameDesc,
        [Display(Name = "Sort by address A-Z")]
        AddressAsc,
        [Display(Name = "Sort by address Z-A")]
        AddressDesc,
        [Display(Name = "Sort by floor")]
        FloorAsc
    }
}
