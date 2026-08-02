namespace MyMusicMaestro.Models;

public class HomeModel
{
    public List<Project> RecentProjects { get; set; } = new();
    public string Greeting { get; set; } = "Welcome to MyMusicMaestro";
}
