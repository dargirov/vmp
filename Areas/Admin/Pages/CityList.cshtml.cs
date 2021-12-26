using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VeganMap.Models;

namespace VeganMap.Areas.Admin.Pages;

public class CityListModel : PageModel
{
    private readonly ApplicationDbContext _dbContext;
    public IList<City> Cities { get; set; }
    public bool HasSuccessMessage { get; set; }

    public CityListModel(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Cities = await _dbContext.Cities.OrderByDescending(x => x.Id).ToListAsync();
        return Page();
    }

    public async Task<IActionResult> OnGetDeleteAsync(int id)
    {
        var city = await _dbContext.Cities.FindAsync(id);
        if (city == null)
        {
            return RedirectToPage("CityList");
        }

        HasSuccessMessage = true;
        city.IsDeleted = true;
        city.DeletedOn = DateTime.Now;
        await _dbContext.SaveChangesAsync();

        return RedirectToPage("CityList");
    }
}
