#nullable disable

using University.Web.Validations;

namespace University.Web.Models.StudentViewModels
{
    public class StudentCreateViewModel
    {
        public string NameFirstName { get; set; }
        public string NameLastName { get; set; }
        public string Email { get; set; }

        [CheckStreetNr(10)]
        public string AdressStreet { get; set; }
        public string AdressZipCode { get; set; }
        public string AdressCity { get; set; }

    }
}
