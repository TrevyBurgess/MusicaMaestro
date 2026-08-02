using MyMusicMaestro.Models;

namespace MyMusicMaestro.ViewModels;

public class TrackViewModel
{
    public TrackViewModel(Track track)
    {
        Title = track.Title;
        Duration = track.Duration;
        Instrument = track.Instrument;
        IsSelected = track.IsSelected;
    }

    public string Title { get; }
    public string Duration { get; }
    public string Instrument { get; }
    public bool IsSelected { get; }
}
