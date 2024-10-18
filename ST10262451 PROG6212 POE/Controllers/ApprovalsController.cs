using Microsoft.AspNetCore.Mvc;

namespace YourProject.Controllers
{
    public class ApprovalsController : Controller
    {
        private readonly IClaimService _claimService;

        public ApprovalsController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        // GET: Approvals/Index (View pending claims)
        public async Task<IActionResult> Index()
        {
            var pendingClaims = await _claimService.GetPendingClaimsAsync();
            return View(pendingClaims);
        }

        // POST: Approvals/Approve
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            await _claimService.ApproveClaimAsync(id);
            return RedirectToAction("Index");
        }

        // POST: Approvals/Reject
        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            await _claimService.RejectClaimAsync(id);
            return RedirectToAction("Index");
        }
    }
}
