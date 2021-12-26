using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VeganMap.Models;

namespace VeganMap.Areas.Admin.Pages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _dbContext;
    public ICollection<Feedback> Feedbacks { get; set; }
    public ICollection<Log> Logs { get; set; }

    public IndexModel(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Feedbacks = await _dbContext.Feedbacks.OrderByDescending(x => x.Id).ToListAsync();
        Logs = await _dbContext.Logs.OrderByDescending(x => x.Id).ToListAsync();

        return Page();
    }

    public async Task<IActionResult> OnGetDeleteFeedbackAsync(int id)
    {
        var feedback = await _dbContext.Feedbacks.FindAsync(id);
        if (feedback != null)
        {
            feedback.IsDeleted = true;
            feedback.DeletedOn = DateTime.Now;
            await _dbContext.SaveChangesAsync();
        }

        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnGetMarkFeedbackAsync(int id)
    {
        var feedback = await _dbContext.Feedbacks.FindAsync(id);
        if (feedback != null)
        {
            feedback.IsNew = false;
            await _dbContext.SaveChangesAsync();
        }

        return RedirectToPage("Index");
    }
}
