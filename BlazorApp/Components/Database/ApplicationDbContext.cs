using MudBlazorApp.Components.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace MudBlazorApp.Components.Database
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<MAccountInfo> AccountInfos { get; set; }
        public DbSet<MUserLevelRight> UserLevelRights { get; set; }
    }
}
