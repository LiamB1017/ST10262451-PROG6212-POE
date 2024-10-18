using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YourProject.Models;
using YourProject.Services;

namespace YourProject.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly IClaimService _claimService;

        public ClaimsController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        // GET: Claims/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Claims/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Claim claim)
        {
            if (ModelState.IsValid)
            {
                await _claimService.SubmitClaimAsync(claim);
                return RedirectToAction("Index", "Claims");
            }
            return View(claim);
        }

        // GET: Claims/Index (View all submitted claims)
        public async Task<IActionResult> Index()
        {
            var claims = await _claimService.GetAllClaimsAsync();
            return View(claims);
        }
    }
}
