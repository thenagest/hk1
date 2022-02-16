using Microsoft.EntityFrameworkCore;

namespace AwaraIt.Hackathon.Models;

public partial class HackathonContext : DbContext
{
    public HackathonContext(DbContextOptions<HackathonContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    /// <summary>
    /// Images
    /// </summary>
    public DbSet<Image> Images { get; set; }

    /// <summary>
    /// Comments
    /// </summary>
    public DbSet<Comment> Comments { get; set; }

    /// <summary>
    /// Likes
    /// </summary>
    public DbSet<Like> Likes { get; set; }

}