namespace CyberFeedForward.MusicaMaestro.Models;

public class SettingsModel
{
    public bool IsDarkTheme { get; set; }
    public bool AreNotificationsEnabled { get; set; } = true;
    public int DefaultTempo { get; set; } = 120;
}
