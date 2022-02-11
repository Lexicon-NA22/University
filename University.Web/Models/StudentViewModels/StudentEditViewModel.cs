namespace University.Web.Models.StudentViewModels;

#nullable disable
public class StudentEditViewModel
{
    public int Id { get; set; }
    public string NameFirstName { get; set; }

    public string NameLastName { get; set; }
    public string Email { get; set; }

    public string AdressStreet { get; set; }
    public string AdressZipCode { get; set; }
    public string AdressCity { get; set; }
}
