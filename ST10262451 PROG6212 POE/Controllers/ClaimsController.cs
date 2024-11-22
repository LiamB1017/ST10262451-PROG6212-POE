using Microsoft.AspNetCore.Mvc;

public class ClaimsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ClaimsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Lecturer submits claim
    [HttpGet]
    public IActionResult SubmitClaim()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SubmitClaim(Claim claim, IFormFile supportingDocument)
    {
        if (ModelState.IsValid)
        {
            // Auto-calculate total amount
            claim.TotalAmount = claim.HoursWorked * claim.HourlyRate;

            if (supportingDocument != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", supportingDocument.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await supportingDocument.CopyToAsync(stream);
                }
                claim.SupportingDocument = "/uploads/" + supportingDocument.FileName;
            }

            claim.Status = "Pending";
            claim.DateSubmitted = DateTime.Now;

            _context.Add(claim);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        return View(claim);
    }

    // Coordinator/Manager reviews claims
    [HttpGet]
    public async Task<IActionResult> ReviewClaims()
    {
        var claims = await _context.Claims.Where(c => c.Status == "Pending").ToListAsync();
        return View(claims);
    }

    [HttpPost]
    public async Task<IActionResult> ApproveClaim(int id)
    {
        var claim = await _context.Claims.FindAsync(id);
        if (claim != null)
        {
            claim.Status = "Approved";
            _context.Update(claim);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("ReviewClaims");
    }

    [HttpPost]
    public async Task<IActionResult> RejectClaim(int id)
    {
        var claim = await _context.Claims.FindAsync(id);
        if (claim != null)
        {
            claim.Status = "Rejected";
            _context.Update(claim);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("ReviewClaims");
    }
}
