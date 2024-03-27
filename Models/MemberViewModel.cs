using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CoronaManagementSystem.Models
{
    //model for the member creat view. has the CovidResultDates filds, a list of al existing vaccinations and a list of VaccinatedModel instead of Vaccinated
    public class MemberViewModel
    {
        [Display(Name = "שם פרטי")]
        [Required(ErrorMessage = "שדה חובה")]
        public string FirstName { get; set; }
        [Display(Name = "שם משפחה")]
        [Required(ErrorMessage = "שדה חובה")]
        public string LastName { get; set; }
        [Display(Name = "ת.ז")]
        [Required(ErrorMessage = "שדה חובה")]
        [PersonId(ErrorMessage = "תעודת זהות לא חוקית")]
        public string Id { get; set; }
        [Display(Name = "עיר")]
        [Required(ErrorMessage = "שדה חובה")]
        public string City { get; set; }
        [Display(Name = "רחוב")]
        [Required(ErrorMessage = "שדה חובה")]
        public string StreetName { get; set; }
        [Display(Name = "מספר בית")]
        [Required(ErrorMessage = "שדה חובה")]
        public string StreetNumber { get; set; }
        [Display(Name = "תאריך לידה")]
        [Required(ErrorMessage = "שדה חובה")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "טלפון")]
        [IsraeliPhone(ErrorMessage = "טלפון ביתי לא חוקי")]
        public string? Phone { get; set; }
        [Display(Name = "טלפון נייד")]
        [Required(ErrorMessage = "שדה חובה")]
        [IsraeliMobilePhone(ErrorMessage = "טלפון נייד לא חוקי")]
        public string Mobile { get; set; }
        [Display(Name = "רשימת חיסונים")]
        public List<Vaccination>? AllVaccinations { get; set; } 
        public List<VaccinatedModel>? Vaccinations { get; set; } = new List<VaccinatedModel>();
        [Display(Name = "תאריך קבלת תשובה חיובית")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PositiveResultDate { get; set; }

        [Display(Name = "מועד החלמה")]
        [AfterPositiveResult("PositiveResultDate", ErrorMessage = "מועד החלמה חייב להיות אחרי תאריך קבלת תשובה חיובית")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? NegativeResultDate { get; set; }
    }

    //has VaccinationId instead of Vaccination 
    public class VaccinatedModel
    {
        public int VaccinationId { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime VaccinationDate { get; set; }
    }
}

