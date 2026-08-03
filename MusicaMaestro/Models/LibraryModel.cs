namespace CyberFeedForward.MusicaMaestro.Models;

public class LibraryModel
{
    public List<Track> Tracks { get; set; } = new();
    public string SearchQuery { get; set; } = string.Empty;
}
