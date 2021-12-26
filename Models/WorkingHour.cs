using System.ComponentModel.DataAnnotations;

namespace VeganMap.Models;

public class WorkingHour : BaseModel
{
    [Required]
    public Place Place { get; set; }

    [Required]
    public int FromHour { get; set; }

    [Required]
    public int FromMinute { get; set; }

    [Required]
    public int ToHour { get; set; }

    [Required]
    public int ToMinute { get; set; }

    [Required]
    public DayOfWeek DayOfWeek { get; set; }
}
