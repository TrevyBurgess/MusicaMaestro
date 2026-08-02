using MyMusicMaestro.Models;

namespace MyMusicMaestro.Services;

public class ProjectService
{
    public List<Project> GetRecentProjects()
    {
        return new List<Project>
        {
            new() { Name = "Summer Vibes", LastModified = "Today", TrackCount = 8 },
            new() { Name = "Lo-Fi Beats", LastModified = "Yesterday", TrackCount = 12 },
            new() { Name = "Symphony No. 5", LastModified = "Last week", TrackCount = 24 }
        };
    }

    public Project CreateProject(string name)
    {
        return new Project { Name = name, LastModified = "Just now", TrackCount = 0 };
    }
}
