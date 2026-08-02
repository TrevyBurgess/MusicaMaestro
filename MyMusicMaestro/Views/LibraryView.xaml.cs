using Microsoft.UI.Xaml.Controls;
using MyMusicMaestro.ViewModels;

namespace MyMusicMaestro.Views;

public sealed partial class LibraryView : Page
{
    public LibraryViewModel ViewModel { get; } = new LibraryViewModel();

    public LibraryView()
    {
        InitializeComponent();
    }
}
