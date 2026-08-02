using MyMusicMaestro.Models;

namespace MyMusicMaestro.ViewModels;

public class AboutViewModel : ViewModelBase
{
    private readonly AboutModel _model;

    public AboutViewModel()
    {
        _model = new AboutModel();
        Title = "About";
        Version = $"Version {_model.Version}";
    }

    public string Title { get; }
    public string Version { get; }
}
