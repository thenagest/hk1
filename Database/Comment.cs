using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AwaraIt.Hackathon.Models;

public class Comment
{
    /// <summary>
    /// ID
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Message
    /// </summary>
    [Required]
    [StringLength(2000, MinimumLength = 1)]
    public string Message { get; set; }

    /// <summary>
    /// Image ID
    /// </summary>
    [Required]
    public Guid ImageId { get; set; }

    /// <summary>
    /// Image
    /// </summary>
    public Image Image { get; set; }

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
}