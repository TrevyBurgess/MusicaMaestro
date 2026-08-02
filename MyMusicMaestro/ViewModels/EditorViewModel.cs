using System.Collections.ObjectModel;
using MyMusicMaestro.Models;
using MyMusicMaestro.Services;

namespace MyMusicMaestro.ViewModels;

public class EditorViewModel : ViewModelBase
{
    private readonly EditorModel _model;
    private readonly MusicService _musicService;
    private int _tempo;

    public EditorViewModel()
    {
        _musicService = new MusicService();
        _model = new EditorModel
        {
            Tracks = _musicService.GetTracks()
        };

        Title = "Editor";
        ProjectName = _model.ProjectName;
        Tempo = _model.Tempo;
        Tracks = new ObservableCollection<TrackViewModel>();
        foreach (var track in _model.Tracks)
        {
            Tracks.Add(new TrackViewModel(track));
        }
    }

    public string Title { get; }
    public string ProjectName { get; set; }

    public int Tempo
    {
        get => _tempo;
        set
        {
            _tempo = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<TrackViewModel> Tracks { get; }
}
