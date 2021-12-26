using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VeganMap.Dtos;
using VeganMap.Models;

namespace VeganMap.Pages;

public class PlaceModel : PageModel
{
    private readonly ApplicationDbContext _dbContext;
    public Place Place { get; set; }
    public string PlacesData { get; set; }

    public PlaceModel(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> OnGetAsync(string acronym)
    {
        ViewData["Cities"] = _dbContext.Cities.Where(x => x.Active).ToList();

        var place = await _dbContext.Places
            .Include(x => x.City)
            .Include(x => x.Pictures)
            .Include(x => x.WorkHours)
            .FirstOrDefaultAsync(x => x.Acronym == acronym);
        if (place == null)
        {
            return NotFound();
        }

        var placesData = new List<PlaceData>
        {
            new PlaceData(place.Lat, place.Lng, (int)place.Type, place.Name, place.Address, place.Acronym)
        };

        PlacesData = JsonSerializer.Serialize(placesData);
        Place = place;
        return Page();
    }
}
