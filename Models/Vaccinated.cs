using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CoronaManagementSystem.Models
{
	public class Vaccinated
	{
		public int Id { get; set; }

		[Display(Name = "סוג חיסון")]
		[Required(ErrorMessage = "שדה חובה")]
		public Vaccination Vaccination { get; set; }
		[Display(Name = "תאריך קבלת חיסון")]
		[Required(ErrorMessage = "שדה חובה")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime VaccinationDate { get; set; }
	}
}
