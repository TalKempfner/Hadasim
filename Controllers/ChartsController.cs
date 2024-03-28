using CoronaManagementSystem2.Data;
using Microsoft.AspNetCore.Mvc;

namespace CoronaManagementSystem.Controllers
{
    public class ChartsController : Controller
    {

        private readonly CoronaManagementSystem2Context _context;

        public ChartsController(CoronaManagementSystem2Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Get data for the current year
            int currentYear = DateTime.Now.Year;
            var dataForCurrentYear = GetDataForYear(currentYear);
            return View(dataForCurrentYear);
        }


        public IActionResult GetChartData(int year)
        {
            var dataForSelectedYear = GetDataForYear(year);
            return Json(dataForSelectedYear);
        }

        // Helper method to get data for a specific year
        private Dictionary<int, int> GetDataForYear(int year)
        {

            var monthlyCounts = new Dictionary<int, int>();
            for (int month = 1; month <= 12; month++)
            {
                DateTime startDate = new DateTime(year, month, 1);
                DateTime endDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                //counts all this month active cases- when the positive result was befor the end of this month and the negative was anter the begining of the month
                int count;
                if (_context.CovidResultDates==null||startDate>DateTime.Now)
                    count=0;
                else
                count = _context.CovidResultDates.Count(d =>
                    ( d.PositiveResultDate <= endDate) &&(!d.NegativeResultDate.HasValue || d.NegativeResultDate.Value >= startDate));
                monthlyCounts.Add(month, count);
            }

            return monthlyCounts;
        }
    }
}
