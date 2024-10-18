using Microsoft.AspNetCore.Mvc;

namespace YourProject.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly IClaimService _claimService;
        private readonly IWebHostEnvironment _environment;

        public ClaimsController(IClaimService claimService, IWebHostEnvironment environment)
        {
            _claimService = claimService;
            _environment = environment;
        }

        // POST: Claims/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Claim claim, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    // File upload logic
                    var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                    var filePath = Path.Combine(uploads, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Save the file path to the claim object
                    claim.SupportingDocumentPath = filePath;
                }

                await _claimService.SubmitClaimAsync(claim);
                return RedirectToAction("Index", "Claims");
            }

            return View(claim);
        }
    }
}
