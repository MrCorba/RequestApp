using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RequestApp.Code.Models;
using RequestApp.Code.Repository;

namespace RequestApp.Pages;

public class AdminModel : PageModel
{
    private readonly ISongRepository _repo;
    public IList<(Song song, int count)> Songs { get; set; } = default!;

    public AdminModel(ISongRepository repo)
    {
        this._repo = repo;
    }

    public async Task OnGetAsync()
    {
        Songs = await this._repo.GetUniqueSongsWithCountAsync();
    }

    public async Task OnPostAsync()
    {
        await this._repo.MarkSongPlayedAsync(Input.Id);
        Songs = await this._repo.GetUniqueSongsWithCountAsync();
    }

    [BindProperty]
    public InputId Input { get; set; } = new();

    public class InputId
    {
        public int Id { get; set; }
    }

    public static string BoolToString(bool val)
    {
        return val ? "yes" : "no";
    }
}
