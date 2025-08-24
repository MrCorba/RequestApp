using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

public class LoginModel : PageModel
{
    [BindProperty]
    public string Username { get; set; }
    [BindProperty]
    public string Password { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? ReturnUrl { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // Basic username/password check (for demo purposes, use secure method in production!)
        if (Username == "user" && Password == "password")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Username)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            if (string.IsNullOrEmpty(this.ReturnUrl))
            {
                return RedirectToPage("/Index");
            }
            return Redirect(this.ReturnUrl);
        }
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return Page();
    }
}