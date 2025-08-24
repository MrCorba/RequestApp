using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RequestApp.Code.Models;

namespace RequestApp.Code.Repository
{
    public class SongRepository : ISongRepository
    {
        private readonly string _connectionString;

        public SongRepository(string dbPath)
        {
            _connectionString = $"Data Source={dbPath}";
        }

        public async Task InitDbAsync()
        {
            using var con = new SqliteConnection(_connectionString);
            await con.OpenAsync();
            var cmd = con.CreateCommand();
            cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS songs (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                artist TEXT NOT NULL,
                title TEXT NOT NULL,
                played INTEGER DEFAULT 0
            )";
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task AddSongAsync(string artist, string title)
        {
            using var con = new SqliteConnection(_connectionString);
            await con.OpenAsync();
            var cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO songs (artist, title) VALUES (@artist, @title)";
            cmd.Parameters.AddWithValue("@artist", artist.Trim());
            cmd.Parameters.AddWithValue("@title", title.Trim());
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<Song>> GetAllSongsAsync()
        {
            var songs = new List<Song>();
            using var con = new SqliteConnection(_connectionString);
            await con.OpenAsync();
            var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT id, artist, title, played FROM songs";
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                songs.Add(new Song
                {
                    Id = reader.GetInt32(0),
                    Artist = reader.GetString(1),
                    Title = reader.GetString(2),
                    Played = reader.GetInt32(3) == 1
                });
            }
            return songs;
        }

        public async Task MarkSongPlayedAsync(int id)
        {
            using var con = new SqliteConnection(_connectionString);
            await con.OpenAsync();
            var cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE songs SET played = 1 WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            await cmd.ExecuteNonQueryAsync();
        }

        // Returns unique songs (artist+title) with count
        public async Task<List<(Song song, int count)>> GetUniqueSongsWithCountAsync()
        {
            var songs = await GetAllSongsAsync();
            var grouped = songs
                .GroupBy(s => (s.Artist.ToLower(), s.Title.ToLower()))
                .Select(g => (song: g.First(), count: g.Count()))
                .ToList();
            return grouped;
        }
    }
}