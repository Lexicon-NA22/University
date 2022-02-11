#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using University.Entities;

namespace University.Data.Data
{
    public class UniversityContext : DbContext
    {
        public DbSet<Student> Student { get; set; }
        public DbSet<Course> Course { get; set; }

        // public DbSet<Enrollment> Enrollment { get; set; }

        public UniversityContext (DbContextOptions<UniversityContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().Property<DateTime>("Edited");

            modelBuilder.Entity<Course>()
                         .ToTable("Course", c => c.IsTemporal());
                             //c =>
                             //{
                             //    c.UseHistoryTable("NewTableName");
                             //    c.HasPeriodStart("NewStart");
                             //    c.HasPeriodEnd("NewEnd");
                             //}
                             //));

            //foreach (var entity in modelBuilder.Model.GetEntityTypes())
            //{
            //    entity.AddProperty("Edited", typeof(DateTime));
            //}

            // modelBuilder.Entity<Student>().OwnsOne(s => s.Name).ToTable("Name");
            modelBuilder.Entity<Student>()
                        .OwnsOne(s => s.Name)
                        .Property(n => n.FirstName)
                        .HasColumnName("FirstName"); 
            
            modelBuilder.Entity<Student>()
                        .OwnsOne(s => s.Name)
                        .Property(n => n.LastName)
                        .HasColumnName("LastName");


           // modelBuilder.Entity<Enrollment>().HasKey(e => new { e.StudentId, e.CourseId });
            modelBuilder.Entity<Course>().Property(c => c.Title).IsRequired();

            modelBuilder.Entity<Student>()
                        .HasMany(s => s.Courses)
                        .WithMany(c => c.Students)
                        .UsingEntity<Enrollment>(
                            e => e.HasOne(e => e.Course).WithMany(c => c.Enrollments),
                            e => e.HasOne(e => e.Student).WithMany(s => s.Enrollments)//,
                           // e => e.HasKey(e => new { e.StudentId, e.CourseId })
                            );

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries<Student>().Where(e => e.State == EntityState.Modified))
            {
                entry.Property("Edited").CurrentValue = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
