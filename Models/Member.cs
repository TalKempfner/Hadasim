using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Reflection;
using System.Security.Policy;
using System.Security.Principal;
using System.Xml.Linq;

namespace CoronaManagementSystem.Models
{
	public class Member
	{ 
		public int Id { get; set; }
		[Display(Name = "שם פרטי")]
		[Required(ErrorMessage = "שדה חובה")]
		public string FirstName { get; set; }
		[Display(Name = "שם משפחה")]
		[Required(ErrorMessage = "שדה חובה")]
		public string LastName { get; set; }
		[Display(Name = "ת.ז")]
		[Required(ErrorMessage = "שדה חובה")]
		[PersonId(ErrorMessage = "תעודת זהות לא חוקית")]
		public string PersonId { get; set; }
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
		public List<Vaccinated>? Vaccinations { get; set; } = new List<Vaccinated>();
		public CovidResultDates? CovidResultDates { get; set; }
		public string? ImageUrl { get; set; }
	}
}
