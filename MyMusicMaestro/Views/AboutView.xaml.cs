using Microsoft.UI.Xaml.Controls;
using MyMusicMaestro.ViewModels;

namespace MyMusicMaestro.Views;

public sealed partial class AboutView : Page
{
    public AboutViewModel ViewModel { get; } = new AboutViewModel();

    public AboutView()
    {
        InitializeComponent();
    }
}
