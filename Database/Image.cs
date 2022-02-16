using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AwaraIt.Hackathon.Models;

public class Image
{
    /// <summary>
    /// ID
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Image's path
    /// </summary>
    [Required]
    [StringLength(2000, MinimumLength = 1)]
    public string Url { get; set; }

    /// <summary>
    /// Date of creation
    /// </summary>
    [Required]
    public DateTime DateOfCreation { get; set; } = DateTime.Now;

    /// <summary>
    /// User ID
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// Is Deleted
    /// </summary>
    [Required]
    public bool Deleted { get; set; } = false;

    public HashSet<Comment> Comments { get; set; }

    public HashSet<Like> Likes { get; set; }
}
