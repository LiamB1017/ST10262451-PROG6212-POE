using Microsoft.AspNetCore.Mvc;

namespace CMCSProject.Controllers
{
    public class ClaimsController : Controller
    {
        // Simulated database for claims
        private static List<Claim> claims = new List<Claim>();

        public IActionResult Index()
        {
            return View(claims); // Display the list of claims
        }

        // GET: Submit New Claim
        public IActionResult Submit()
        {
            return View();
        }

        // POST: Submit Claim
        [HttpPost]
        public IActionResult Submit(Claim claim)
        {
            claim.ClaimID = claims.Count + 1; // Auto-generate ClaimID
            claims.Add(claim); // Add claim to list
            return RedirectToAction("Index");
        }
    }
}
