using LoginApplication.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoginApplication.Model
{
    public class RecordDbContext : IdentityDbContext
    {
        public RecordDbContext(DbContextOptions<RecordDbContext> options) : base(options) { }
        public DbSet<Record> Records { get; set; }
    }
}
