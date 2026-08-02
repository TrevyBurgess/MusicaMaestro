using Microsoft.UI.Xaml.Controls;
using MyMusicMaestro.ViewModels;

namespace MyMusicMaestro.Views;

public sealed partial class HomeView : Page
{
    public HomeViewModel ViewModel { get; } = new HomeViewModel();

    public HomeView()
    {
        InitializeComponent();
    }
}
