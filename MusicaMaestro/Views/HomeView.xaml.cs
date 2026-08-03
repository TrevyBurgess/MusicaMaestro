using Microsoft.UI.Xaml.Controls;
using CyberFeedForward.MusicaMaestro.ViewModels;

namespace CyberFeedForward.MusicaMaestro.Views;

public sealed partial class HomeView : Page
{
    public HomeViewModel ViewModel { get; } = new HomeViewModel();

    public HomeView()
    {
        InitializeComponent();
    }
}
