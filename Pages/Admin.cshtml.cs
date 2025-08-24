using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RequestApp.Code.Models;
using RequestApp.Code.Repository;

namespace RequestApp.Pages;


public class AdminModel : PageModel
{
    private readonly ISongRepository _repo;
    public IList<Song> Songs { get;set; } = default!;

    public AdminModel(ISongRepository repo)
    {
        this._repo = repo;
    }

     public async Task OnGetAsync()
        {
            await this._repo.AddSongAsync("Title", "Artist");

            Songs = await this._repo.GetAllSongsAsync();
        }
}
