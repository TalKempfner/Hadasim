using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CoronaManagementSystem.Models
{
	public class Vaccination
	{
		[Display(Name = "קוד")]
		public int Id { get; set; }
		[Display(Name = "יצרן")]
		[Required(ErrorMessage = "שדה חובה")]
		public string Manufacturer { get; set; }
	}
}
