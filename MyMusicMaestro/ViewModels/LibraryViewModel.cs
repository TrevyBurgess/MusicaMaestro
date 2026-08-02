using System.Collections.ObjectModel;
using MyMusicMaestro.Models;
using MyMusicMaestro.Services;

namespace MyMusicMaestro.ViewModels;

public class LibraryViewModel : ViewModelBase
{
    private readonly LibraryModel _model;
    private readonly MusicService _musicService;
    private string _searchQuery = string.Empty;

    public LibraryViewModel()
    {
        _musicService = new MusicService();
        _model = new LibraryModel
        {
            Tracks = _musicService.GetTracks()
        };

        Title = "Library";
        Tracks = new ObservableCollection<TrackViewModel>();
        foreach (var track in _model.Tracks)
        {
            Tracks.Add(new TrackViewModel(track));
        }
    }

    public string Title { get; }

    public ObservableCollection<TrackViewModel> Tracks { get; }

    public string SearchQuery
    {
        get => _searchQuery;
        set
        {
            _searchQuery = value;
            OnPropertyChanged();
            FilterTracks();
        }
    }

    private void FilterTracks()
    {
        Tracks.Clear();
        foreach (var track in _musicService.GetTracks())
        {
            if (string.IsNullOrWhiteSpace(_searchQuery) ||
                track.Title.Contains(_searchQuery, System.StringComparison.OrdinalIgnoreCase) ||
                track.Instrument.Contains(_searchQuery, System.StringComparison.OrdinalIgnoreCase))
            {
                Tracks.Add(new TrackViewModel(track));
            }
        }
    }
}
