namespace CyberFeedForward.MusicaMaestro.Models;

public class HomeModel
{
    public List<Project> RecentProjects { get; set; } = new();
    public string Greeting { get; set; } = "Welcome to CyberFeedForward.MusicaMaestro";
}
