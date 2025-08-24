using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RequestApp.Code.Models;
using RequestApp.Code.Repository;

namespace RequestApp.Pages;

public class IndexModel : PageModel
{
    private readonly ISongRepository _repo;
    [TempData]
    public string? SuccessMessage { get; set; }

    public IndexModel(ISongRepository repo)
    {
        this._repo = repo;
    }

    public async Task OnGetAsync()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await this._repo.AddSongAsync(Input.Artist, Input.Title);
        Input = new();

        SuccessMessage = "Thanks for your request! The DJ has been notified.";
        return RedirectToPage();
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        public string Artist { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}