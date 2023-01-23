using Microsoft.EntityFrameworkCore;
using SportSections.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportSections.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
        {
            // Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Departament> Departaments { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentSection> StudentSections { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<TrainerSection> TrainerSections { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer("Server=DESKTOP-KIV92L3;Database=SportSectionsIHE;Trusted_Connection=True;Encrypt=False;");
             optionsBuilder.UseSqlServer("Server=DESKTOP-I75L3P7;Database=SportSectionsIHE;Trusted_Connection=True;Encrypt=False;");
            // optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SportSectionsIHE;Trusted_Connection=True;");
        }
    }
}
