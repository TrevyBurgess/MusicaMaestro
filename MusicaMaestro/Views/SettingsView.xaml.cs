using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using CyberFeedForward.MusicaMaestro.ViewModels;

namespace CyberFeedForward.MusicaMaestro.Views;

public sealed partial class SettingsView : Page
{
    public SettingsViewModel ViewModel { get; } = new SettingsViewModel();

    public SettingsView()
    {
        InitializeComponent();
        DataContext = ViewModel;
        ViewModel.ThemeChanged += OnThemeChanged;
    }

    private void OnThemeChanged(int themeModeIndex)
    {
        if (Application.Current is App app)
        {
            app.SetTheme(themeModeIndex);
        }
    }

    private async void BrowseSoundClipsButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var picker = new Windows.Storage.Pickers.FolderPicker();
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.MusicLibrary;
            picker.FileTypeFilter.Add("*");

            if (Application.Current is App app && app.MainWindow is not null)
            {
                var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(app.MainWindow);
                WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);
            }

            var folder = await picker.PickSingleFolderAsync();
            if (folder is not null)
            {
                ViewModel.SoundClipsPath = folder.Path;
            }
        }
        catch (Exception ex)
        {
            var dialog = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "Error",
                Content = $"Failed to select the SoundClips folder: {ex.Message}",
                CloseButtonText = "OK"
            };
            await dialog.ShowAsync();
        }
    }
}
