using System.ComponentModel.DataAnnotations;

namespace VeganMap.Models;

public class Place : BaseModel
{
    public Place()
    {
        Pictures = new List<PlacePicture>();
        WorkHours = new List<WorkingHour>();
    }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    [Required]
    [MaxLength(200)]
    public string Acronym { get; set; }

    [Required]
    public bool Active { get; set; }

    [Required]
    [MaxLength(1000)]
    public string PageTitle { get; set; }

    [Required]
    [MaxLength(1000)]
    public string PageDescription { get; set; }

    [Required]
    [MaxLength(1000)]
    public string ShortDescription { get; set; }

    [Required]
    public string FullDescription { get; set; }

    [Required]
    [MaxLength(200)]
    public string Address { get; set; }

    [Required]
    public City City { get; set; }

    public int CityId { get; set; }

    [Range(0, 5)]
    public decimal? Rating { get; set; }

    [Range(0, 3)]
    public decimal? PriceRange { get; set; }

    [Required]
    public PlaceTypeEnum Type { get; set; }

    [MaxLength(20)]
    public string? Phone { get; set; }

    [MaxLength(100)]
    public string? Website { get; set; }

    [MaxLength(100)]
    public string? Facebook { get; set; }

    [MaxLength(100)]
    public string? Instagram { get; set; }

    public ICollection<PlacePicture> Pictures { get; set; }

    [Required]
    [MaxLength(20)]
    public string Lat { get; set; }

    [Required]
    [MaxLength(20)]
    public string Lng { get; set; }

    [MaxLength(100)]
    public string? GoogleMapsShare { get; set; }

    public ICollection<WorkingHour> WorkHours { get; set; }
}
