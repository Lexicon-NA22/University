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
        public Student Student { get; set; }
    }
}
