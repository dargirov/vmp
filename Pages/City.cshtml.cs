using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VeganMap.Dtos;
using VeganMap.Models;

namespace VeganMap.Pages;

public class CityModel : PageModel
{
    private readonly ApplicationDbContext _dbContext;

    public City City { get; set; }
    public ICollection<Place> Places { get; set; }
    public string PlacesData { get; set; }

    public CityModel(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> OnGetAsync(string acronym)
    {
        ViewData["Cities"] = _dbContext.Cities.Where(x => x.Active).ToList();

        var city = await _dbContext.Cities.FirstOrDefaultAsync(x => x.Acronym == acronym);
        if (city == null)
        {
            return RedirectToPage("Index");
        }

        var placesData = new List<PlaceData>();
        var places = await _dbContext.Places.Include(x => x.City).Where(x => x.Active && x.City == city).ToListAsync();
        foreach (var place in places)
        {
            placesData.Add(new PlaceData(place.Lat, place.Lng, (int)place.Type, place.Name, place.Address, place.Acronym));
        }

        PlacesData = JsonSerializer.Serialize(placesData);
        Places = places;
        City = city;

        return Page();
    }
}
