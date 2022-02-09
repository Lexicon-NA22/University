using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Entities;

namespace University.Data.Data
{
    public class SeedData
    {
        private static Faker faker;

        public static async Task InitAsync(UniversityContext db)
        {
            if (await db.Student.AnyAsync()) return;

            faker = new Faker("sv");

            var students = GetStudents();
            await db.AddRangeAsync(students);

            await db.SaveChangesAsync();
        }

        private static IEnumerable<Student> GetStudents()
        {
            var students = new List<Student>();

            for (int i = 0; i < 200; i++)
            {
                var fName = faker.Name.FirstName();
                var lName = faker.Name.LastName();

                var student = new Student
                {
                    FirstName = fName,
                    LastName = lName,
                    Email = faker.Internet.Email($"{fName} {lName}"),
                    Avatar = faker.Internet.Avatar(),
                    Adress = new Adress
                    {
                        City = faker.Address.City(),
                        Street = faker.Address.StreetAddress(),
                        ZipCode = faker.Address.ZipCode()
                    }
                };

                students.Add(student);
            }
            return students;
        }
    }
}
