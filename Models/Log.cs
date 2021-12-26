using System.ComponentModel.DataAnnotations;

namespace VeganMap.Models;

public class Log : BaseModel
{
    [Required]
    public LogLevel Level { get; set; }

    [Required]
    [MaxLength(50)]
    public string Code { get; set; }

    [Required]
    public string Content { get; set; }
}
