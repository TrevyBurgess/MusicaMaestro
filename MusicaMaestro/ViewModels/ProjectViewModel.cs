using CyberFeedForward.MusicaMaestro.Models;

namespace CyberFeedForward.MusicaMaestro.ViewModels;

public class ProjectViewModel
{
    public ProjectViewModel(Project project)
    {
        Name = project.Name;
        LastModified = project.LastModified;
        TrackCount = project.TrackCount;
    }

    public string Name { get; }
    public string LastModified { get; }
    public int TrackCount { get; }
}
