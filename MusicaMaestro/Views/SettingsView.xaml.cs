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
            var picker = new Windows.Storage.Pickers.FolderPicker
            {
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.MusicLibrary
            };
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

    private void ResetToDefaultsButton_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.Reset();
    }

    private async void OpenSoundClipsFolderButton_Click(object sender, RoutedEventArgs e)
    {
        var path = ViewModel.SoundClipsPath;
        if (string.IsNullOrWhiteSpace(path) || !System.IO.Directory.Exists(path))
        {
            var dialog = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "SoundClips folder not found",
                Content = "The SoundClips folder does not exist. Please browse for a valid folder first.",
                CloseButtonText = "OK"
            };
            await dialog.ShowAsync();
            return;
        }

        try
        {
            System.Diagnostics.Process.Start("explorer.exe", path);
        }
        catch (Exception ex)
        {
            var dialog = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "Error",
                Content = $"Failed to open the SoundClips folder: {ex.Message}",
                CloseButtonText = "OK"
            };
            await dialog.ShowAsync();
        }
    }
}
