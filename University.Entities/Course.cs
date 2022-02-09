using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace University.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }

        //Nav prop
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
