using Microsoft.UI.Xaml.Controls;
using MyMusicMaestro.ViewModels;

namespace MyMusicMaestro.Views;

public sealed partial class EditorView : Page
{
    public EditorViewModel ViewModel { get; } = new EditorViewModel();

    public EditorView()
    {
        InitializeComponent();
    }
}
