using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//#nullable disable

namespace University.Entities
{
    public class Student 
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public Name Name { get; set; }
        public string Email { get; set; }

        //Nav prop
        public Adress Adress { get; set; } = new Adress();

        //Con 2
        //public ICollection<Enrollment> Enrollments { get; set; }

        //Con 3 - Con 1 + Con 2
        //Con 4
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public ICollection<Course> Courses { get; set; } = new List<Course>();

        private Student()
        {
            Avatar = null!;
            Name = null!;
            Email = null!;
        }

        public Student(string avatar, string email, Name name)
        {
            Avatar = avatar;
            Name = name;
            Email = email;
        }

    }
}
