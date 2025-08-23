using System.Collections.Generic;
using System.Threading.Tasks;
using RequestApp.Code.Models;

namespace RequestApp.Code.Repository {

public interface ISongRepository
{
    Task InitDbAsync();
    Task AddSongAsync(string artist, string title);
    Task<List<Song>> GetAllSongsAsync();
    Task MarkSongPlayedAsync(int id);
    Task<List<(Song song, int count)>> GetUniqueSongsWithCountAsync();
}
}