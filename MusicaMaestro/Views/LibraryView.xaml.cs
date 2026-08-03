using Microsoft.UI.Xaml.Controls;
using CyberFeedForward.MusicaMaestro.ViewModels;

namespace CyberFeedForward.MusicaMaestro.Views;

public sealed partial class LibraryView : Page
{
    public LibraryViewModel ViewModel { get; } = new LibraryViewModel();

    public LibraryView()
    {
        InitializeComponent();
    }
}
