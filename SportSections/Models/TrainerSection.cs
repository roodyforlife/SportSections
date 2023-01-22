using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportSections.Models
{
    public class TrainerSection
    {
        public int TrainerSectionId { get; set; }
        public Section Section { get; set; }
        public int SectionId { get; set; }
        public Trainer Trainer { get; set; }
        public int TrainerId { get; set; }
    }
}
