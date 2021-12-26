using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VeganMap;
using VeganMap.Models;

namespace veganmap.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    private readonly ApplicationDbContext _dbContext;
    public string? ExceptionMessage { get; set; }

    public ErrorModel(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task OnGetAsync()
    {
        ViewData["Cities"] = _dbContext.Cities.Where(x => x.Active).ToList();

        var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
        {
            ExceptionMessage = "404 страницата не е намерена";
        }
        else
        {
            ExceptionMessage = "Системна грешка";
        }

        var log = new Log
        {
            Code = exceptionHandlerPathFeature?.Error.GetType().Name ?? "unknown",
            Content = exceptionHandlerPathFeature?.Error.ToString() ?? string.Empty,
            CreatedOn = DateTime.Now,
            Level = LogLevel.Error,
        };

        _dbContext.Logs.Add(log);
        await _dbContext.SaveChangesAsync();
    }
}
