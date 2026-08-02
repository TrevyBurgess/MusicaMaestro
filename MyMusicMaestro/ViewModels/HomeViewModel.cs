using System.Collections.ObjectModel;
using System.Windows.Input;
using MyMusicMaestro.Models;
using MyMusicMaestro.Services;

namespace MyMusicMaestro.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private readonly HomeModel _model;
    private readonly ProjectService _projectService;

    public HomeViewModel()
    {
        _projectService = new ProjectService();
        _model = new HomeModel
        {
            RecentProjects = _projectService.GetRecentProjects()
        };

        Title = _model.Greeting;
        RecentProjects = new ObservableCollection<ProjectViewModel>();
        foreach (var project in _model.RecentProjects)
        {
            RecentProjects.Add(new ProjectViewModel(project));
        }

        CreateProjectCommand = new RelayCommand(CreateProject);
    }

    public string Title { get; }
    public ObservableCollection<ProjectViewModel> RecentProjects { get; }
    public ICommand CreateProjectCommand { get; }

    private void CreateProject()
    {
        var project = _projectService.CreateProject("New Project");
        RecentProjects.Insert(0, new ProjectViewModel(project));
    }
}
