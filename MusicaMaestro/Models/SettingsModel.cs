using Windows.Storage;

namespace CyberFeedForward.MusicaMaestro.Models;

public class SettingsModel
{
    public const string ThemeModeIndexKey = "ThemeModeIndex";
    public const string AreNotificationsEnabledKey = "AreNotificationsEnabled";
    public const string DefaultTempoKey = "DefaultTempo";
    public const string SoundClipsPathKey = "SoundClipsPath";
    public const string NavigationPaneWidthKey = "NavigationPaneWidth";
    public const string IsNavigationPaneOpenKey = "IsNavigationPaneOpen";
    public const string MainWindowXKey = "MainWindowX";
    public const string MainWindowYKey = "MainWindowY";
    public const string MainWindowWidthKey = "MainWindowWidth";
    public const string MainWindowHeightKey = "MainWindowHeight";
    public const string FirstRunCompletedKey = "FirstRunCompletedKey";

    public SettingsModel()
    {
        var settings = ApplicationData.Current.LocalSettings;
        _themeModeIndex = GetValue(settings, ThemeModeIndexKey, 0);
        _areNotificationsEnabled = GetValue(settings, AreNotificationsEnabledKey, true);
        _defaultTempo = GetValue(settings, DefaultTempoKey, 120);

        var defaultSoundClipsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "SoundClips");

        _soundClipsPath = GetValue(settings, SoundClipsPathKey, defaultSoundClipsPath);

        _navigationPaneWidth = GetValue(settings, NavigationPaneWidthKey, 150.0);
        _isNavigationPaneOpen = GetValue(settings, IsNavigationPaneOpenKey, true);
        _mainWindowX = GetValue(settings, MainWindowXKey, 0);
        _mainWindowY = GetValue(settings, MainWindowYKey, 0);
        _mainWindowWidth = GetValue(settings, MainWindowWidthKey, 0);
        _mainWindowHeight = GetValue(settings, MainWindowHeightKey, 0);

        _FirstRunCompleted = GetValue(settings, FirstRunCompletedKey, false);
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
    private int _themeModeIndex;

    public bool AreNotificationsEnabled
    {
        get => _areNotificationsEnabled;
        set
        {
            _areNotificationsEnabled = value;
            Save(AreNotificationsEnabledKey, value);
        }
    }
    private bool _areNotificationsEnabled;

    public int DefaultTempo
    {
        get => _defaultTempo;
        set
        {
            _defaultTempo = value;
            Save(DefaultTempoKey, value);
        }
    }
    private int _defaultTempo = 120;

    public string SoundClipsPath
    {
        get
        {
            return _soundClipsPath;
        }
        set
        {
            _soundClipsPath = value;
            Save(SoundClipsPathKey, value);
        }
    }
    private string _soundClipsPath;

    public double NavigationPaneWidth
    {
        get => _navigationPaneWidth;
        set
        {
            _navigationPaneWidth = value;
            Save(NavigationPaneWidthKey, value);
        }
    }
    private double _navigationPaneWidth;

    public bool IsNavigationPaneOpen
    {
        get => _isNavigationPaneOpen;
        set
        {
            _isNavigationPaneOpen = value;
            Save(IsNavigationPaneOpenKey, value);
        }
    }
    private bool _isNavigationPaneOpen;

    public int MainWindowX
    {
        get => _mainWindowX;
        set
        {
            _mainWindowX = value;
            Save(MainWindowXKey, value);
        }
    }
    private int _mainWindowX;

    public int MainWindowY
    {
        get => _mainWindowY;
        set
        {
            _mainWindowY = value;
            Save(MainWindowYKey, value);
        }
    }
    private int _mainWindowY;

    public int MainWindowWidth
    {
        get => _mainWindowWidth;
        set
        {
            _mainWindowWidth = value;
            Save(MainWindowWidthKey, value);
        }
    }
    private int _mainWindowWidth;

    public int MainWindowHeight
    {
        get => _mainWindowHeight;
        set
        {
            _mainWindowHeight = value;
            Save(MainWindowHeightKey, value);
        }
    }
    private int _mainWindowHeight;

    public bool FirstRunCompleted
    {
        get => _FirstRunCompleted;
        set
        {
            _FirstRunCompleted = value;
            Save(FirstRunCompletedKey, value);
        }
    }
    private bool _FirstRunCompleted;

    public void ResetToDefaults()
    {
        ThemeModeIndex = 0;
        AreNotificationsEnabled = true;
        DefaultTempo = 120;
        SoundClipsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "SoundClips");
        NavigationPaneWidth = 150.0;
        FirstRunCompleted = true;
        MainWindowX = 0;
        MainWindowY = 0;
        MainWindowWidth = 0;
        MainWindowHeight = 0;
    }

    private static void Save(string key, object value)
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
