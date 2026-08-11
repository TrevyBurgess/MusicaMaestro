using CyberFeedForward.MusicaMaestro.Models;
using CyberFeedForward.MusicaMaestro.ViewModels;
using CyberFeedForward.MusicaMaestro.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;

namespace CyberFeedForward.MusicaMaestro.Services;

public class FirstRunService
{
    public static bool IsFirstRunCompleted => GetIsCompleted();

    public static async Task InitializeAsync(Window? window)
    {
        if (window?.Content is null || IsFirstRunCompleted)
        {
            return;
        }

        if (window.Content.XamlRoot is null && window.Content is FrameworkElement rootElement)
        {
            var tcs = new TaskCompletionSource();
            void handler(object _1, RoutedEventArgs _2)
            {
                rootElement.Loaded -= handler;
                tcs.TrySetResult();
            }

            rootElement.Loaded += handler;
            await tcs.Task;
        }

        var viewModel = new SettingsViewModel();
        viewModel.ThemeChanged += OnThemeChanged;

        var dialog = new FirstRunDialog(viewModel)
        {
            XamlRoot = window.Content.XamlRoot
        };

        await dialog.ShowAsync();

        var soundClipsPath = viewModel.SoundClipsPath;
        if (!string.IsNullOrWhiteSpace(soundClipsPath))
        {
            try
            {
                System.IO.Directory.CreateDirectory(soundClipsPath);
            }
            catch (Exception ex)
            {
                var errorDialog = new ContentDialog
                {
                    XamlRoot = window.Content.XamlRoot,
                    Title = "Could not create SoundClips folder",
                    Content = $"The SoundClips folder could not be created: {ex.Message}",
                    CloseButtonText = "OK"
                };
                await errorDialog.ShowAsync();
            }
        }
        SetIsCompleted(true);
    }

    private static void OnThemeChanged(int themeModeIndex)
    {
        if (Application.Current is App app)
        {
            app.SetTheme(themeModeIndex);
        }
    }

    private static bool GetIsCompleted()
    {
        var settings = ApplicationData.Current.LocalSettings;
        if (settings.Values.TryGetValue(SettingsModel.FirstRunCompletedKey, out var value) && value is bool completed)
        {
            return completed;
        }

        return false;
    }

    private static void SetIsCompleted(bool completed)
    {
        var settings = ApplicationData.Current.LocalSettings;
        settings.Values[SettingsModel.FirstRunCompletedKey] = completed;
    }
}
