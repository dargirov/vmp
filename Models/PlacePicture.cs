using System.ComponentModel.DataAnnotations;

namespace VeganMap.Models;

public class PlacePicture : BaseModel
{
    [Required]
    [MaxLength(255)]
    public string FullSizeFilename { get; set; }

    [Required]
    public int FullSizeWidth { get; set; }

    [Required]
    public int FullSizeHeight { get; set; }

    [Required]
    [MaxLength(255)]
    public string ThumbFilename { get; set; }

    [Required]
    public int ThumbWidth { get; set; }

    [Required]
    public int ThumbHeight { get; set; }

    [Required]
    public Place Place { get; set; }

    public int PlaceId { get; set; }

    [Required]
    public int Sort { get; set; }
}
