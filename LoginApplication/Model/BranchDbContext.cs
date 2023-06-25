using LoginApplication.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoginApplication.Model
{
    public class BranchDbContext : IdentityDbContext
    {
        public BranchDbContext(DbContextOptions<BranchDbContext> options) : base(options) { }
        public DbSet<Branch> Branches { get; set; }
    }
}
