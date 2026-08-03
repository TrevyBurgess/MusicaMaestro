using Microsoft.UI.Xaml.Controls;
using CyberFeedForward.MusicaMaestro.ViewModels;

namespace CyberFeedForward.MusicaMaestro.Views;

public sealed partial class AboutView : Page
{
    public AboutViewModel ViewModel { get; } = new AboutViewModel();

    public AboutView()
    {
        InitializeComponent();
    }
}
