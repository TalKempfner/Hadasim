using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace CoronaManagementSystem.Models
{
	public class CovidResultDates
	{
		public int Id { get; set; } 

		[Display(Name = "תאריך קבלת תשובה חיובית")]
		[Required(ErrorMessage = "שדה חובה")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PositiveResultDate { get; set; }

		[Display(Name = "מועד החלמה")]
		[AfterPositiveResult("PositiveResultDate", ErrorMessage = "מועד החלמה חייב להיות אחרי תאריך קבלת תשובה חיובית")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? NegativeResultDate { get; set; }
	}
}
