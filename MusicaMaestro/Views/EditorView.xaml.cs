using Microsoft.UI.Xaml.Controls;
using CyberFeedForward.MusicaMaestro.ViewModels;

namespace CyberFeedForward.MusicaMaestro.Views;

public sealed partial class EditorView : Page
{
    public EditorViewModel ViewModel { get; } = new EditorViewModel();

    public EditorView()
    {
        InitializeComponent();
    }
}
