using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolunteerManagementSystem.Data;
using VolunteerManagementSystem.Models;

namespace VolunteerManagementSystem.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VolunteerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // List all volunteers (with optional filtering + search) 
        public async Task<IActionResult> Index(string filter, string category, string searchString)
        {
            var volunteers = from v in _context.Volunteers
                             select v;

            // Status filter
            if (!string.IsNullOrEmpty(filter) && filter != "All")
            {
                if (filter == "ApprovedPending")
                {
                    volunteers = volunteers.Where(v => v.Status == VolunteerStatus.Approved || v.Status == VolunteerStatus.PendingApproval);
                }
                else if (Enum.TryParse<VolunteerStatus>(filter, out var statusFilter))
                {
                    volunteers = volunteers.Where(v => v.Status == statusFilter);
                }
            }

            // Category filter (hardcoded values)
            if (!string.IsNullOrEmpty(category) && category != "All")
            {
                volunteers = volunteers.Where(v => v.InterestCategory == category);
            }

            // Search filter
            if (!string.IsNullOrEmpty(searchString))
            {
                volunteers = volunteers.Where(v =>
                    v.FirstName.Contains(searchString) ||
                    v.LastName.Contains(searchString) ||
                    v.Email.Contains(searchString));
            }

            // Preserve current selections
            ViewData["CurrentFilter"] = filter ?? "All";
            ViewData["CurrentCategory"] = category ?? "All";
            ViewData["CurrentSearch"] = searchString;

            return View(await volunteers.ToListAsync());
        }



        // GET: Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var volunteer = await _context.Volunteers
                                          .FirstOrDefaultAsync(v => v.Id == id);
            if (volunteer == null) return NotFound();

            return View(volunteer);
        }

        // GET: Create
        public IActionResult Create()
        {
            return View(new Volunteer());
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Status,InterestCategory")] Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(volunteer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(volunteer);
        }


        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer == null) return NotFound();

            return View(volunteer);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Volunteer volunteer)
        {
            if (id != volunteer.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingVolunteer = await _context.Volunteers.FindAsync(id);
                    if (existingVolunteer == null) return NotFound();

                    // Update all properties
                    existingVolunteer.FirstName = volunteer.FirstName;
                    existingVolunteer.LastName = volunteer.LastName;
                    existingVolunteer.Username = volunteer.Username;
                    existingVolunteer.Password = volunteer.Password;
                    existingVolunteer.CentersPreferred = volunteer.CentersPreferred;
                    existingVolunteer.SkillsInterests = volunteer.SkillsInterests;
                    existingVolunteer.Availability = volunteer.Availability;
                    existingVolunteer.Address = volunteer.Address;
                    existingVolunteer.HomePhone = volunteer.HomePhone;
                    existingVolunteer.WorkPhone = volunteer.WorkPhone;
                    existingVolunteer.CellPhone = volunteer.CellPhone;
                    existingVolunteer.Email = volunteer.Email;
                    existingVolunteer.EducationalBackground = volunteer.EducationalBackground;
                    existingVolunteer.CurrentLicenses = volunteer.CurrentLicenses;
                    existingVolunteer.EmergencyContactName = volunteer.EmergencyContactName;
                    existingVolunteer.EmergencyContactPhone = volunteer.EmergencyContactPhone;
                    existingVolunteer.EmergencyContactEmail = volunteer.EmergencyContactEmail;
                    existingVolunteer.EmergencyContactAddress = volunteer.EmergencyContactAddress;
                    existingVolunteer.HasDriversLicenseOnFile = volunteer.HasDriversLicenseOnFile;
                    existingVolunteer.HasSSCardOnFile = volunteer.HasSSCardOnFile;
                    existingVolunteer.InterestCategory = volunteer.InterestCategory;
                    existingVolunteer.Status = volunteer.Status;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Volunteers.Any(v => v.Id == volunteer.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(volunteer);
        }


        // GET: Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var volunteer = await _context.Volunteers
                                          .FirstOrDefaultAsync(v => v.Id == id);
            if (volunteer == null) return NotFound();

            return View(volunteer);
        }

        // POST: Delete (confirmed)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer != null)
            {
                _context.Volunteers.Remove(volunteer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Matches
        public async Task<IActionResult> Matches(int id)
        {
            var volunteer = await _context.Volunteers
                .FirstOrDefaultAsync(v => v.Id == id);

            if (volunteer == null)
            {
                return NotFound();
            }

            var matches = await _context.VolunteerOpportunityMatches
                .Include(m => m.Opportunity)
                .Where(m => m.VolunteerId == id)
                .Select(m => m.Opportunity)
                .ToListAsync();

            ViewData["VolunteerName"] = $"{volunteer.FirstName} {volunteer.LastName}";
            return View(matches);
        }

    }
}
