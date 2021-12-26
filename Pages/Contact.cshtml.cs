using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VeganMap.Pages;

public class ContactModel : PageModel
{
    private readonly ApplicationDbContext _dbContext;

    [Required]
    [MaxLength(100)]
    [BindProperty]
    public string Name { get; set; }

    [Required]
    [MaxLength(100)]
    [BindProperty]
    public string Email { get; set; }

    [Required]
    [MaxLength(1000)]
    [BindProperty]
    public string Content { get; set; }

    [TempData]
    public bool HasSuccessMessage { get; set; }
    public bool HasErrorMessage { get; set; }

    public ContactModel(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void OnGet()
    {
        ViewData["Cities"] = _dbContext.Cities.Where(x => x.Active).ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ViewData["Cities"] = _dbContext.Cities.Where(x => x.Active).ToList();

        if (!ModelState.IsValid)
        {
            HasErrorMessage = true;
            return Page();
        }

        HasSuccessMessage = true;
        _dbContext.Feedbacks.Add(new Models.Feedback
        {
            Name = Name,
            Email = Email,
            Content = Content,
            CreatedOn = DateTime.Now,
            IsNew = true,
        });
        await _dbContext.SaveChangesAsync();

        return RedirectToPage("Contact");
    }
}
