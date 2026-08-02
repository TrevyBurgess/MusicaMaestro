using MyMusicMaestro.Models;

namespace MyMusicMaestro.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private readonly SettingsModel _model;

    public SettingsViewModel()
    {
        _model = new SettingsModel();
        Title = "Settings";
        IsDarkTheme = _model.IsDarkTheme;
        AreNotificationsEnabled = _model.AreNotificationsEnabled;
        DefaultTempo = _model.DefaultTempo;
    }

    public string Title { get; }

    public bool IsDarkTheme
    {
        get => _model.IsDarkTheme;
        set
        {
            _model.IsDarkTheme = value;
            OnPropertyChanged();
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
}
