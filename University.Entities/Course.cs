using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace University.Entities
{
    public class Course
    {
        public int Id { get; set; }

        //[Required]
        public string Title { get; set; }

        //Nav prop
        public ICollection<Enrollment> Enrollments { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
