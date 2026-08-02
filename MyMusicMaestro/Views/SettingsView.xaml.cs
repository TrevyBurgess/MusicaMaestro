using Microsoft.UI.Xaml.Controls;
using MyMusicMaestro.ViewModels;

namespace MyMusicMaestro.Views;

public sealed partial class SettingsView : Page
{
    public SettingsViewModel ViewModel { get; } = new SettingsViewModel();

    public SettingsView()
    {
        InitializeComponent();
    }
}
