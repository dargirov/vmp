using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VeganMap.Dtos;
using VeganMap.Models;

namespace VeganMap.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ApplicationDbContext _dbContext;

    public ICollection<Place> Places { get; set; }
    public string PlacesData { get; set; }

    public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task OnGetAsync()
    {
        ViewData["Cities"] = _dbContext.Cities.Where(x => x.Active).ToList();

        var placesData = new List<PlaceData>();
        var places = await _dbContext.Places.Include(x => x.City).Where(x => x.Active).ToListAsync();
        foreach (var place in places)
        {
            placesData.Add(new PlaceData(place.Lat, place.Lng, (int)place.Type, place.Name, place.Address, place.Acronym));
        }

        PlacesData = JsonSerializer.Serialize(placesData);
        Places = places;
    }
}
