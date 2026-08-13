using System.Collections.ObjectModel;
using System.IO;
using Microsoft.UI.Dispatching;
using CyberFeedForward.MusicaMaestro.Models;
using CyberFeedForward.MusicaMaestro.Services;

namespace CyberFeedForward.MusicaMaestro.ViewModels;

public class LibraryViewModel : ViewModelBase, IDisposable
{
    private readonly LibraryModel _model;
    private readonly MusicService _musicService;
    private readonly FileSystemWatcher? _fileSystemWatcher;
    private readonly DispatcherQueue _dispatcherQueue;
    private string _searchQuery = string.Empty;

    public LibraryViewModel()
    {
        _dispatcherQueue = DispatcherQueue.GetForCurrentThread();
        _musicService = new MusicService();
        _model = new LibraryModel
        {
            Tracks = _musicService.GetTracks()
        };

        var settings = new SettingsModel();
        var path = settings.SoundClipsPath;
        if (!string.IsNullOrWhiteSpace(path) && Directory.Exists(path))
        {
            _fileSystemWatcher = new FileSystemWatcher(path)
            {
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName,
                IncludeSubdirectories = false
            };

            _fileSystemWatcher.Created += OnFileSystemChanged;
            _fileSystemWatcher.Deleted += OnFileSystemChanged;
            _fileSystemWatcher.Renamed += OnFileSystemRenamed;
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        Title = "Library";
        Tracks = new ObservableCollection<TrackViewModel>();
        FilterTracks();
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

    private void OnFileSystemChanged(object sender, FileSystemEventArgs e)
    {
        _dispatcherQueue.TryEnqueue(FilterTracks);
    }

    private void OnFileSystemRenamed(object sender, RenamedEventArgs e)
    {
        _dispatcherQueue.TryEnqueue(FilterTracks);
    }

    public void Dispose()
    {
        if (_fileSystemWatcher is not null)
        {
            _fileSystemWatcher.EnableRaisingEvents = false;
            _fileSystemWatcher.Created -= OnFileSystemChanged;
            _fileSystemWatcher.Deleted -= OnFileSystemChanged;
            _fileSystemWatcher.Renamed -= OnFileSystemRenamed;
            _fileSystemWatcher.Dispose();
        }
    }
}
