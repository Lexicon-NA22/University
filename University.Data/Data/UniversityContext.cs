#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using University.Entities;

namespace University.Data.Data
{
    public class UniversityContext : DbContext
    {
        public DbSet<Student> Student { get; set; }
       // public DbSet<Enrollment> Enrollment { get; set; }

        public UniversityContext (DbContextOptions<UniversityContext> options)
            : base(options)
        {
        }

    }
}
