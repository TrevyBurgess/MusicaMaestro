using Microsoft.UI.Xaml.Controls;
using CyberFeedForward.MusicaMaestro.ViewModels;

namespace CyberFeedForward.MusicaMaestro.Views;

public sealed partial class SettingsView : Page
{
    public SettingsViewModel ViewModel { get; } = new SettingsViewModel();

    public SettingsView()
    {
        InitializeComponent();
    }
}
