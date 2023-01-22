using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportSections.Models
{
    public class StudentSection
    {
        public int StudentSectionId { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }
        public Section Section { get; set; }
        public int SectionId { get; set; }
    }
}
