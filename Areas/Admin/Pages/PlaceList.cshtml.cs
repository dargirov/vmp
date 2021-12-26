using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VeganMap.Models;

namespace VeganMap.Areas.Admin.Pages;

public class PlaceListModel : PageModel
{
    private readonly ApplicationDbContext _dbContext;

    public IList<Place> Places { get; set; }
    [TempData]
    public bool HasSuccessMessage { get; set; }

    public PlaceListModel(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Places = await _dbContext.Places.Include(x => x.City).OrderByDescending(x => x.Id).ToListAsync();
        return Page();
    }

    public async Task<IActionResult> OnGetDeleteAsync(int id)
    {
        var place = await _dbContext.Places
            .Include(x => x.Pictures)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (place == null)
        {
            return RedirectToPage("PlaceList");
        }

        place.IsDeleted = true;
        place.DeletedOn = DateTime.Now;

        foreach (var picture in place.Pictures)
        {
            picture.DeletedOn = DateTime.Now;
            picture.IsDeleted = true;
        }

        HasSuccessMessage = true;
        await _dbContext.SaveChangesAsync();

        return RedirectToPage("PlaceList");
    }
}
