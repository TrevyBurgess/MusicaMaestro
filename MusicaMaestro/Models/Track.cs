namespace CyberFeedForward.MusicaMaestro.Models;

public class Track
{
    public string Title { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public string Instrument { get; set; } = string.Empty;
    public bool IsSelected { get; set; }
}
