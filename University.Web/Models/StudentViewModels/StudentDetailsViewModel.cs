
using University.Entities;

#nullable disable

namespace University.Web.Models.StudentViewModels
{
    public class StudentDetailsViewModel
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string NameFirstName { get; set; }
        public string NameLastName { get; set; }
        public string Email { get; set; }
        public string AdressStreet { get; set; }
        public string AdressZipCode { get; set; }
        public string AdressCity { get; set; }
        public int Attending { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}
