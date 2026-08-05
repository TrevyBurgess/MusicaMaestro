using CyberFeedForward.MusicaMaestro.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CyberFeedForward.MusicaMaestro.Views;

public sealed partial class FirstRunDialog : ContentDialog
{
    public FirstRunDialog()
    {
        InitializeComponent();
        ViewModel = new SettingsViewModel();
        DataContext = ViewModel;
    }

    public FirstRunDialog(SettingsViewModel viewModel)
        : this()
    {
        ViewModel = viewModel;
        DataContext = viewModel;
    }

    public SettingsViewModel ViewModel { get; private set; } = null!;

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
            var errorDialog = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "Error",
                Content = $"Failed to select the SoundClips folder: {ex.Message}",
                CloseButtonText = "OK"
            };
            await errorDialog.ShowAsync();
        }
    }
}
