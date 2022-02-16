
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AwaraIt.Hackathon.Identity
{
    public class HackathonIdentityContext : IdentityDbContext<HackathonUser, HackathonRole, string>
    {
        public HackathonIdentityContext(DbContextOptions<HackathonIdentityContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}