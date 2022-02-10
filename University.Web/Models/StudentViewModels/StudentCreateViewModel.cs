#nullable disable

namespace University.Web.Models.StudentViewModels
{
    public class StudentCreateViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AdressStreet { get; set; }
        public string AdressZipCode { get; set; }
        public string AdressCity { get; set; }

    }
}
