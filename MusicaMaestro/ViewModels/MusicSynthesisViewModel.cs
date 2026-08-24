using System.Windows.Input;
using CyberFeedForward.MusicaMaestro.Models;
using CyberFeedForward.MusicaMaestro.Services;

namespace CyberFeedForward.MusicaMaestro.ViewModels;

public class MusicSynthesisViewModel : ViewModelBase
{
    private readonly AiService _aiService;
    private readonly SettingsModel _settings;
    private string _query = string.Empty;
    private string _response = string.Empty;
    private string _errorMessage = string.Empty;
    private bool _isGenerating;

    public MusicSynthesisViewModel()
    {
        Title = "Music Synthesis";
        _aiService = new AiService();
        _settings = new SettingsModel();
        GenerateCommand = new RelayCommand(() => _ = GenerateAsync(), () => CanGenerate);
    }

    public string Title { get; }

    public ICommand GenerateCommand { get; }

    public string Query
    {
        get => _query;
        set
        {
            _query = value;
            OnPropertyChanged();
            ((RelayCommand)GenerateCommand).RaiseCanExecuteChanged();
        }
    }

    public string Response
    {
        get => _response;
        set
        {
            _response = value;
            OnPropertyChanged();
        }
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged();
        }
    }

    public bool IsGenerating
    {
        get => _isGenerating;
        set
        {
            _isGenerating = value;
            OnPropertyChanged();
            ((RelayCommand)GenerateCommand).RaiseCanExecuteChanged();
        }
    }

    public bool IsAiConfigured =>
        !string.IsNullOrWhiteSpace(_settings.AiEndpoint) &&
        (_settings.AiProvider == 0 || (!string.IsNullOrWhiteSpace(_settings.AiModel) && !string.IsNullOrWhiteSpace(_settings.AiApiKey)));

    public bool IsAiNotConfigured => !IsAiConfigured;

    public bool CanGenerate => IsAiConfigured && !IsGenerating && !string.IsNullOrWhiteSpace(Query);

    public async Task GenerateAsync()
    {
        if (!CanGenerate)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(_settings.AiEndpoint))
        {
            ErrorMessage = "AI endpoint is not configured. Set it in Settings.";
            return;
        }

        ErrorMessage = string.Empty;
        Response = string.Empty;
        IsGenerating = true;

        try
        {
            Response = await _aiService.GenerateAsync(
                Query,
                _settings.AiEndpoint,
                _settings.AiModel,
                _settings.AiApiKey);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"AI request failed: {ex.Message}";
        }
        finally
        {
            IsGenerating = false;
        }
    }
}
