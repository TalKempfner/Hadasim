using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoronaManagementSystem.Models;
using CoronaManagementSystem2.Data;
using System.Net;

namespace CoronaManagementSystem2.Controllers
{
    public class MembersController : Controller
    {
        private readonly CoronaManagementSystem2Context _context;

        public MembersController(CoronaManagementSystem2Context context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            return _context.Member != null ?
                        View(await _context.Member.ToListAsync()) :
                        Problem("Entity set 'CoronaManagementSystem2Context.Member'  is null.");
        }
        // GET: Members/Search
        public IActionResult Search(string searchString)
        {
            // Retrieve all members from the database
            var members = _context.Member.ToList();

            // If a search string is provided, filter the members
            if (!string.IsNullOrEmpty(searchString))
            {
                // Filter members whose FirstName or PersonId starts with the search string
                members = members.Where(m => m.FirstName.StartsWith(searchString) || m.PersonId.StartsWith(searchString)).ToList();
            }
            return View("Index", members);
        }


        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var member = await _context.Member.Include(m => m.Vaccinations)?.ThenInclude(v => v.Vaccination).Include(m => m.CovidResultDates).FirstOrDefaultAsync(m => m.Id == id);

            if (member == null)
            {
                return NotFound();
            }
            MemberDetailsModel memberDetailsModel = new MemberDetailsModel();
            memberDetailsModel.StreetNumber = member.StreetNumber;
            memberDetailsModel.City = member.City;
            memberDetailsModel.StreetName= member.StreetName;
            memberDetailsModel.Mobile= member.Mobile;
            memberDetailsModel.Phone= member.Phone;
            memberDetailsModel.FirstName= member.FirstName;
            memberDetailsModel.LastName= member.LastName;
            memberDetailsModel.DateOfBirth= member.DateOfBirth;
            memberDetailsModel.Id=member.PersonId;
            memberDetailsModel.PositiveResultDate= member.CovidResultDates?.PositiveResultDate;
            memberDetailsModel.NegativeResultDate= member.CovidResultDates?.NegativeResultDate;
            memberDetailsModel.ImageUrl= member.ImageUrl;
            if (member.Vaccinations != null)
            {
                VaccinatedDetailModel vaccinatedDetailModel = new VaccinatedDetailModel();
                foreach (var v in member.Vaccinations)
                {
                    vaccinatedDetailModel = new VaccinatedDetailModel();
                    vaccinatedDetailModel.VaccinationDate = v.VaccinationDate;
                    vaccinatedDetailModel.VaccinationDescription = v.Vaccination.Manufacturer + " - " + v.Vaccination.Id;
                    memberDetailsModel.Vaccinations.Add(vaccinatedDetailModel);
                }
            }
            return View(memberDetailsModel);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            MemberViewModel memberViewModel = new MemberViewModel();
            memberViewModel.AllVaccinations= _context.Vaccination?.ToList();
            return View(memberViewModel);
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Id,City,StreetName,StreetNumber,DateOfBirth,Phone,Mobile,Vaccinations,NegativeResultDate,PositiveResultDate,ImageFile")] MemberViewModel memberViewModel)
        {
            //copy data from MemberViewModel to Member
            Member member = new Member();
            member.FirstName = memberViewModel.FirstName;
            member.LastName = memberViewModel.LastName;
            member.PersonId= memberViewModel.Id;
            member.City= memberViewModel.City;
            member.StreetName= memberViewModel.StreetName;
            member.StreetNumber= memberViewModel.StreetNumber;
            member.DateOfBirth= memberViewModel.DateOfBirth;
            member.Phone= memberViewModel.Phone;
            member.Mobile= memberViewModel.Mobile;
            Vaccinated vaccinated;
            foreach (var v in memberViewModel.Vaccinations)
            {
                vaccinated= new Vaccinated();
                vaccinated.Vaccination = await _context.Vaccination.FindAsync(v.VaccinationId);
                vaccinated.VaccinationDate = v.VaccinationDate;
                member.Vaccinations.Add(vaccinated);
            }
            if (memberViewModel.PositiveResultDate.HasValue)
            {
                member.CovidResultDates= new CovidResultDates();
                member.CovidResultDates.PositiveResultDate = memberViewModel.PositiveResultDate.Value;
                member.CovidResultDates.NegativeResultDate = memberViewModel.NegativeResultDate;
            }
            //profile image
            if (memberViewModel.ImageFile != null && memberViewModel.ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(memberViewModel.ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profile", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await memberViewModel.ImageFile.CopyToAsync(stream);
                }

                // Save the file path to the database
                member.ImageUrl = "/images/profile/" + fileName;
            }
            //add Member
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            memberViewModel.AllVaccinations= _context.Vaccination?.ToList();
            return View(memberViewModel);
        }
       
        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var member = await _context.Member.Include(m => m.Vaccinations)?.ThenInclude(v => v.Vaccination).Include(m => m.CovidResultDates).FirstOrDefaultAsync(m => m.Id == id);
            var memberEditModel = new MemberEditModel();
            memberEditModel.Id= member.Id;
            memberEditModel.FirstName=member.FirstName;
            memberEditModel.LastName=member.LastName;
            memberEditModel.PersonId = member.PersonId;
            memberEditModel.StreetNumber=member.StreetNumber;
            memberEditModel.City=member.City;
            memberEditModel.StreetName=member.StreetName;
            memberEditModel.DateOfBirth=member.DateOfBirth;
            memberEditModel.Mobile=member.Mobile;
            memberEditModel.Phone=member.Phone;
            if (member.CovidResultDates!=null)
            {
                memberEditModel.PositiveResultDate=member.CovidResultDates.PositiveResultDate;
                memberEditModel.NegativeResultDate=member.CovidResultDates.NegativeResultDate;
            }
            memberEditModel.AllVaccinations = _context.Vaccination?.ToList();
            VaccinatedModel vaccinatedModel;
            foreach (var v in member.Vaccinations)
            {
                vaccinatedModel= new VaccinatedModel();
                vaccinatedModel.VaccinationDate=v.VaccinationDate;
                vaccinatedModel.VaccinationId= v.Vaccination.Id;
                memberEditModel.Vaccinations.Add(vaccinatedModel);
            }
            if (member == null)
            {
                return NotFound();
            }
            return View(memberEditModel);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,Id,PersonId,City,StreetName,StreetNumber,DateOfBirth,Phone,Mobile,Vaccinations,NegativeResultDate,PositiveResultDate,ImageFile")] MemberEditModel memberEditModel)
        {
            if (id != memberEditModel.Id)
            {
                return NotFound();
            }
            var member = await _context.Member.Include(m => m.Vaccinations)?.ThenInclude(v => v.Vaccination).Include(m => m.CovidResultDates).FirstOrDefaultAsync(m => m.Id == id);
            member.FirstName = memberEditModel.FirstName;
            member.LastName = memberEditModel.LastName;
            member.PersonId= memberEditModel.PersonId;
            member.City= memberEditModel.City;
            member.StreetName= memberEditModel.StreetName;
            member.StreetNumber= memberEditModel.StreetNumber;
            member.DateOfBirth= memberEditModel.DateOfBirth;
            member.Phone= memberEditModel.Phone;
            member.Mobile= memberEditModel.Mobile;
            Vaccinated vaccinated;
            Vaccinated newV;
            for (int i = 0; i < memberEditModel.Vaccinations.Count; i++)
            { 
                //update the existing vaccinations in member
                if (i < member.Vaccinations.Count)
                {
                    member.Vaccinations[i].Vaccination =await _context.Vaccination.FindAsync(memberEditModel.Vaccinations[i].VaccinationId);
                    member.Vaccinations[i].VaccinationDate = memberEditModel.Vaccinations[i].VaccinationDate;
                }
                else if (memberEditModel.Vaccinations[i].VaccinationDate!= default)
                {
                    // Add new vaccination if member.Vaccinations is shorter than memberEditModel.Vaccinations
                    newV = new Vaccinated();
                    newV.Vaccination = await _context.Vaccination.FindAsync(memberEditModel.Vaccinations[i].VaccinationId);
                    newV.VaccinationDate = memberEditModel.Vaccinations[i].VaccinationDate;
                    member.Vaccinations.Add(newV);
                }
            }
            //if the original member didn't have CovidResultDates and now has, add a new CovidResultDates
            if (member.CovidResultDates==null && memberEditModel.PositiveResultDate.HasValue)
            {
                member.CovidResultDates= new CovidResultDates();
                member.CovidResultDates.PositiveResultDate = memberEditModel.PositiveResultDate.Value;
                member.CovidResultDates.NegativeResultDate = memberEditModel.NegativeResultDate;
            }
            //if the original member had CovidResultDates, update them
            if (member.CovidResultDates!=null)
            {
                member.CovidResultDates.PositiveResultDate = memberEditModel.PositiveResultDate.Value;
                member.CovidResultDates.NegativeResultDate = memberEditModel.NegativeResultDate;
            }
            //profile image
            if (memberEditModel.ImageFile != null && memberEditModel.ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(memberEditModel.ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profile", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await memberEditModel.ImageFile.CopyToAsync(stream);
                }

                // Save the file path to the database
                member.ImageUrl = "/images/profile/" + fileName;
            }
            for (int i=0 ;i<4; i++)
            {
                if (memberEditModel.Vaccinations[i].VaccinationDate == default)
                {
                    ModelState.Remove("Vaccinations["+i+"].VaccinationDate");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            memberEditModel.AllVaccinations= _context.Vaccination?.ToList();
            return View(memberEditModel);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Member == null)
            {
                return Problem("Entity set 'CoronaManagementSystem2Context.Member'  is null.");
            }
            var member = _context.Member.Include(m => m.Vaccinations).Include(m => m.CovidResultDates).SingleOrDefault(m => m.Id == id);
            if (member != null)
            {
                //delete dependent data
                if (member.CovidResultDates != null)
                    _context.CovidResultDates?.Remove(member.CovidResultDates);
                if (member.Vaccinations != null)
                    _context.Vaccinated?.RemoveRange(member.Vaccinations);
                _context.Member.Remove(member);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return (_context.Member?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
