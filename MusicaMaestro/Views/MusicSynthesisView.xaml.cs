using Microsoft.UI.Xaml.Controls;
using CyberFeedForward.MusicaMaestro.ViewModels;

namespace CyberFeedForward.MusicaMaestro.Views;

public sealed partial class MusicSynthesisView : Page
{
    public MusicSynthesisViewModel ViewModel { get; } = new MusicSynthesisViewModel();

    public MusicSynthesisView()
    {
        InitializeComponent();
    }
}
