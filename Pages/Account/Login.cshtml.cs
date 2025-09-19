using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class LoginModel : PageModel
{
    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Password { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? ReturnUrl { get; set; }

    private readonly IConfiguration _configuration;

    public LoginModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (
            Username != "admin"
            || string.IsNullOrEmpty(Password)
            || Password != _configuration["AdminPassword"]
        )
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, Username) };
        var claimsIdentity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity)
        );

        if (string.IsNullOrEmpty(this.ReturnUrl))
        {
            return RedirectToPage("/Index");
        }
        return Redirect(this.ReturnUrl);
    }
}
