namespace RequestApp.Code.Models{
public class Song
{
    public int Id { get; set; }
    public string Artist { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public bool Played { get; set; }
}
}