using Windows.Storage;

namespace CyberFeedForward.MusicaMaestro.Models;

public class SettingsModel
{
    private const string ThemeModeIndexKey = "ThemeModeIndex";
    private const string AreNotificationsEnabledKey = "AreNotificationsEnabled";
    private const string DefaultTempoKey = "DefaultTempo";
    private const string SoundClipsPathKey = "SoundClipsPath";
    private const string NavigationPaneWidthKey = "NavigationPaneWidth";
    private const string IsNavigationPaneOpenKey = "IsNavigationPaneOpen";
    private const string MainWindowXKey = "MainWindowX";
    private const string MainWindowYKey = "MainWindowY";
    private const string MainWindowWidthKey = "MainWindowWidth";
    private const string MainWindowHeightKey = "MainWindowHeight";

    private int _themeModeIndex;
    private bool _areNotificationsEnabled = true;
    private int _defaultTempo = 120;
    private string _soundClipsPath = string.Empty;
    private double _navigationPaneWidth = 320.0;
    private bool _isNavigationPaneOpen = true;
    private int _mainWindowX;
    private int _mainWindowY;
    private int _mainWindowWidth;
    private int _mainWindowHeight;

    public SettingsModel()
    {
        Load();
    }

    public int ThemeModeIndex
    {
        get => _themeModeIndex;
        set
        {
            _themeModeIndex = value;
            Save(ThemeModeIndexKey, value);
        }
    }

    public bool AreNotificationsEnabled
    {
        get => _areNotificationsEnabled;
        set
        {
            _areNotificationsEnabled = value;
            Save(AreNotificationsEnabledKey, value);
        }
    }

    public int DefaultTempo
    {
        get => _defaultTempo;
        set
        {
            _defaultTempo = value;
            Save(DefaultTempoKey, value);
        }
    }

    public string SoundClipsPath
    {
        get => _soundClipsPath;
        set
        {
            _soundClipsPath = value;
            Save(SoundClipsPathKey, value);
        }
    }

    public double NavigationPaneWidth
    {
        get => _navigationPaneWidth;
        set
        {
            _navigationPaneWidth = value;
            Save(NavigationPaneWidthKey, value);
        }
    }

    public bool IsNavigationPaneOpen
    {
        get => _isNavigationPaneOpen;
        set
        {
            _isNavigationPaneOpen = value;
            Save(IsNavigationPaneOpenKey, value);
        }
    }

    public int MainWindowX
    {
        get => _mainWindowX;
        set
        {
            _mainWindowX = value;
            Save(MainWindowXKey, value);
        }
    }

    public int MainWindowY
    {
        get => _mainWindowY;
        set
        {
            _mainWindowY = value;
            Save(MainWindowYKey, value);
        }
    }

    public int MainWindowWidth
    {
        get => _mainWindowWidth;
        set
        {
            _mainWindowWidth = value;
            Save(MainWindowWidthKey, value);
        }
    }

    public int MainWindowHeight
    {
        get => _mainWindowHeight;
        set
        {
            _mainWindowHeight = value;
            Save(MainWindowHeightKey, value);
        }
    }

    private void Load()
    {
        var settings = ApplicationData.Current.LocalSettings;
        _themeModeIndex = GetValue(settings, ThemeModeIndexKey, 0);
        _areNotificationsEnabled = GetValue(settings, AreNotificationsEnabledKey, true);
        _defaultTempo = GetValue(settings, DefaultTempoKey, 120);
        _soundClipsPath = GetValue(settings, SoundClipsPathKey, string.Empty);
        _navigationPaneWidth = GetValue(settings, NavigationPaneWidthKey, 320.0);
        _isNavigationPaneOpen = GetValue(settings, IsNavigationPaneOpenKey, true);
        _mainWindowX = GetValue(settings, MainWindowXKey, 0);
        _mainWindowY = GetValue(settings, MainWindowYKey, 0);
        _mainWindowWidth = GetValue(settings, MainWindowWidthKey, 0);
        _mainWindowHeight = GetValue(settings, MainWindowHeightKey, 0);
    }

    private void Save(string key, object value)
    {
        var settings = ApplicationData.Current.LocalSettings;
        settings.Values[key] = value;
    }

    private static T GetValue<T>(ApplicationDataContainer settings, string key, T defaultValue)
    {
        if (settings.Values.TryGetValue(key, out object? value) && value is T typedValue)
        {
            return typedValue;
        }

        return defaultValue;
    }
}
