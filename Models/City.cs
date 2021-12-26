using System.ComponentModel.DataAnnotations;

namespace VeganMap.Models;

public class City : BaseModel
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public string Acronym { get; set; }

    [Required]
    [MaxLength(1000)]
    public string PageTitle { get; set; }

    [MaxLength(1000)]
    public string PageDescription { get; set; }

    [Required]
    public bool Active { get; set; }
}
