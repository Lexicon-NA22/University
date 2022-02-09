using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace University.Entities
{
    public class Adress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        //Foregn key
        public int StudentId { get; set; }

        //Nav prop
        public Student Student { get; set; }
    }
}
