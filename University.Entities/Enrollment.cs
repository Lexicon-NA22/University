using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace University.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int Grade { get; set; }

        //Convention 1 - En student kan finnas på många olika Enrollments i tabellen
        //               Kommer generera ett nullable int foreign key som ett shadow property
        //public Student Student { get; set; }

        //Con 3 - Con1  + Con 2
        //public Student Student { get; set; }

        //Con 4
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }


    }
}
