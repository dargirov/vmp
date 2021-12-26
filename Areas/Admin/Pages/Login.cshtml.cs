using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VeganMap.Areas.Admin.Pages;

public class LoginModel : PageModel
{
    private readonly IConfiguration _configuration;

    [BindProperty]
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [BindProperty]
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public LoginModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var usersData = _configuration.GetSection("Users").Get<IList<UserData>>();

        if (ModelState.IsValid)
        {
            foreach (var userData in usersData)
            {
                if (Email == userData.Email && Password == userData.Password)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, Email),
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToPage("Index");
                }
            }
        }

        return RedirectToPage("Login");
    }

    private class UserData
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public UserData()
        {
        }

        public UserData(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
