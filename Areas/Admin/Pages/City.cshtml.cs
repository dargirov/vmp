using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VeganMap.Models;

namespace VeganMap.Areas.Admin.Pages;

public class CityModel : PageModel
{
    private readonly ApplicationDbContext _dbContext;

    [BindProperty]
    public City City { get; set; }
    public SelectList NoYesOptions { get; private set; }
    public bool IsCreate => !City?.IsSaved ?? true;
    public bool IsEdit => !IsCreate;
    [TempData]
    public bool HasSuccessMessage { get; set; }
    public bool HasErrorMessage { get; set; }

    public CityModel(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        InitSelectLists();

        if (id == null)
        {
            City = new City();
        }
        else
        {
            var city = await _dbContext.Cities.FindAsync(id.Value);
            City = city;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        InitSelectLists();

        if (!ModelState.IsValid)
        {
            HasErrorMessage = true;
            return Page();
        }

        if (!City.IsSaved)
        {
            City.CreatedOn = DateTime.Now;
            _dbContext.Cities.Add(City);
        }
        else
        {
            _dbContext.Cities.Update(City);
        }


        HasSuccessMessage = true;
        await _dbContext.SaveChangesAsync();

        return RedirectToPage("City", new { id = City.Id });
    }

    private void InitSelectLists()
    {
        NoYesOptions = new SelectList(new SelectListItem[] { new SelectListItem("Не", "False"), new SelectListItem("Да", "True") }, nameof(SelectListItem.Value), nameof(SelectListItem.Text));
    }
}
