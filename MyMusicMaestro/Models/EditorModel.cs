namespace MyMusicMaestro.Models;

public class EditorModel
{
    public string ProjectName { get; set; } = "Untitled Project";
    public int Tempo { get; set; } = 120;
    public List<Track> Tracks { get; set; } = new();
}
