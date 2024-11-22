public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    { }

    public DbSet<Claim> Claims { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
}

public class ApplicationUser
{
}

public class DbSet<T>
{
}

public class DbContext
{
    public DbContext(DbContextOptions<ApplicationDbContext> options)
    {
    }
}

public class DbContextOptions<T>
{
}