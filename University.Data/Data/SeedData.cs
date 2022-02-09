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
        private static Faker faker = null!;

        public static async Task InitAsync(UniversityContext db)
        {
            if (await db.Student.AnyAsync()) return;

            faker = new Faker("sv");

            var students = GetStudents();
            await db.AddRangeAsync(students);

            var courses = GetCourses();
            await db.AddRangeAsync(courses);

            var enrollments = GetEnrollments(students, courses);
            await db.AddRangeAsync(enrollments);


            await db.SaveChangesAsync();
        }

        private static IEnumerable<Enrollment> GetEnrollments(IEnumerable<Student> students, IEnumerable<Course> courses)
        {
            var enrollments = new List<Enrollment>();

            foreach (var student in students)
            {
                foreach (var course in courses)
                {
                    if (faker.Random.Int(0, 5) == 0)
                    {
                        var enrollment = new Enrollment
                        {
                            Course = course,
                            Student = student,
                            Grade = faker.Random.Int(1, 5)
                        };
                        enrollments.Add(enrollment);
                    }
                }
            }

            return enrollments;
        }

        private static IEnumerable<Course> GetCourses()
        {
            var courses = new List<Course>();

            for (int i = 0; i < 20; i++)
            {
                var course = new Course(faker.Company.CatchPhrase());
                //{
                //    Title = faker.Company.CatchPhrase()
                //};

                courses.Add(course);
            }

            return courses;
        }

        private static IEnumerable<Student> GetStudents()
        {
            var students = new List<Student>();

            for (int i = 0; i < 200; i++)
            {
                var fName = faker.Name.FirstName();
                var lName = faker.Name.LastName();
                var avatar = faker.Internet.Avatar();
                var email = faker.Internet.Email($"{fName} {lName}");

                var student = new Student(avatar, email, new Name(fName, lName))
                {
                    //FirstName = fName,
                    //LastName = lName,
                    //Email = faker.Internet.Email($"{fName} {lName}"),
                    //Avatar = faker.Internet.Avatar(),
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
