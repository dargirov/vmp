using System.ComponentModel.DataAnnotations;

namespace VeganMap.Models;

public class Feedback : BaseModel
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Content { get; set; }

    [Required]
    public bool IsNew { get; set; }
}
