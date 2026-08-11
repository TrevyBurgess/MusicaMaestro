using CyberFeedForward.MusicaMaestro.Models;

namespace CyberFeedForward.MusicaMaestro.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    private readonly SettingsModel _model;

    public SettingsViewModel()
    {
        _model = new SettingsModel();
        Title = "Settings1";
    }

    public string Title { get; }

    public event Action<int>? ThemeChanged;

    public int ThemeModeIndex
    {
        get => _model.ThemeModeIndex;
        set
        {
            _model.ThemeModeIndex = value;
            OnPropertyChanged();
            ThemeChanged?.Invoke(value);
        }
    }

    public bool AreNotificationsEnabled
    {
        get => _model.AreNotificationsEnabled;
        set
        {
            _model.AreNotificationsEnabled = value;
            OnPropertyChanged();
        }
    }

    public int DefaultTempo
    {
        get => _model.DefaultTempo;
        set
        {
            _model.DefaultTempo = value;
            OnPropertyChanged();
        }
    }

    public string SoundClipsPath
    {
        get => _model.SoundClipsPath;
        set
        {
            _model.SoundClipsPath = value;
            OnPropertyChanged();
        }
    }

    public void Reset()
    {
        _model.ResetToDefaults();

        OnPropertyChanged(nameof(ThemeModeIndex));
        OnPropertyChanged(nameof(AreNotificationsEnabled));
        OnPropertyChanged(nameof(DefaultTempo));
        OnPropertyChanged(nameof(SoundClipsPath));

        ThemeChanged?.Invoke(ThemeModeIndex);
    }
}
