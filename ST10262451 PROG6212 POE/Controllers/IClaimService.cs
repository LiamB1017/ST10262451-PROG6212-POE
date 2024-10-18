public class ClaimService : IClaimService
{
    private readonly YourDbContext _context;

    public ClaimService(YourDbContext context)
    {
        _context = context;
    }

    public async Task SubmitClaimAsync(Claim claim)
    {
        _context.Claims.Add(claim);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Claim>> GetAllClaimsAsync()
    {
        return await _context.Claims.ToListAsync();
    }

    public async Task<IEnumerable<Claim>> GetPendingClaimsAsync()
    {
        return await _context.Claims.Where(c => c.Status == "Pending").ToListAsync();
    }

    public async Task ApproveClaimAsync(int claimId)
    {
        var claim = await _context.Claims.FindAsync(claimId);
        if (claim != null)
        {
            claim.Status = "Approved";
            await _context.SaveChangesAsync();
        }
    }

    public async Task RejectClaimAsync(int claimId)
    {
        var claim = await _context.Claims.FindAsync(claimId);
        if (claim != null)
        {
            claim.Status = "Rejected";
            await _context.SaveChangesAsync();
        }
    }
}

public interface IClaimService
{
}