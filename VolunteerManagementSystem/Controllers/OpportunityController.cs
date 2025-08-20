using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolunteerManagementSystem.Data;
using VolunteerManagementSystem.Models;

namespace VolunteerManagementSystem.Controllers
{
    public class OpportunityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OpportunityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: List all opportunities with optional filter/search
        public async Task<IActionResult> Index(string filter = "All", string searchString = "")
        {
            ViewData["CurrentFilter"] = filter;
            ViewData["CurrentSearch"] = searchString;

            var query = _context.Opportunities.AsQueryable();

            // Example filter logic
            if (!string.IsNullOrEmpty(filter) && filter != "All")
            {
                if (filter == "Most Recent (60 days)")
                {
                    var cutoff = DateTime.Now.AddDays(-60);
                    query = query.Where(o => o.StartDate >= cutoff);
                }
                else if (filter == "By Center")
                {
                    // TODO: filter by center
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(o => o.Title.Contains(searchString) || o.Category.Contains(searchString));
            }

            var opportunities = await query.ToListAsync();

            // In controller before returning the view
            var optionsHtml = "";
            var filters = new[] { "Most Recent (60 days)", "By Center", "All" };
            foreach (var f in filters)
            {
                var selected = f == filter ? "selected" : "";
                optionsHtml += $"<option value=\"{f}\" {selected}>{f}</option>";
            }
            ViewData["FilterOptions"] = optionsHtml;

            return View(opportunities);
        }


        // GET: Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var opportunity = await _context.Opportunities.FirstOrDefaultAsync(o => o.Id == id);
            if (opportunity == null) return NotFound();

            return View(opportunity);
        }

        // GET: Create
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminUser")))
                return RedirectToAction("Login", "Admin");

            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Opportunity opportunity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(opportunity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(opportunity);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminUser")))
                return RedirectToAction("Login", "Admin");

            var opportunity = await _context.Opportunities.FindAsync(id);
            if (opportunity == null) return NotFound();
            return View(opportunity);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Opportunity opportunity)
        {
            if (id != opportunity.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opportunity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Opportunities.Any(o => o.Id == opportunity.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(opportunity);
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var opportunity = await _context.Opportunities.FirstOrDefaultAsync(o => o.Id == id);
            if (opportunity == null) return NotFound();

            return View(opportunity);
        }

        // POST: DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var opportunity = await _context.Opportunities.FindAsync(id);
            if (opportunity != null)
            {
                _context.Opportunities.Remove(opportunity);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Matches for a volunteer
        public async Task<IActionResult> Matches(int id)
        {
            // Get the opportunity
            var opportunity = await _context.Opportunities.FindAsync(id);
            if (opportunity == null) return NotFound();

            // Find volunteers whose InterestCategory matches the Opportunity category
            var matchedVolunteers = await _context.Volunteers
                .Where(v => v.InterestCategory == opportunity.Category)
                .ToListAsync();

            ViewBag.Opportunity = opportunity;
            return View(matchedVolunteers); // Pass matched volunteers to the view
        }

    }
}
