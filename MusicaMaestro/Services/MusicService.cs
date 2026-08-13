using System.IO;
using CyberFeedForward.MusicaMaestro.Models;

namespace CyberFeedForward.MusicaMaestro.Services;

public class MusicService
{
    private static readonly HashSet<string> AudioExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".mp3",
        ".wav",
        ".flac",
        ".aac",
        ".ogg",
        ".wma",
        ".m4a",
        ".aiff",
        ".opus"
    };

    public List<Track> GetTracks()
    {
        var settings = new SettingsModel();
        var path = settings.SoundClipsPath;

        if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
        {
            return new List<Track>();
        }

        try
        {
            return Directory
                .EnumerateFiles(path)
                .Where(file => AudioExtensions.Contains(Path.GetExtension(file)))
                .Select(file => new Track
                {
                    Title = Path.GetFileNameWithoutExtension(file) ?? file,
                    Instrument = (Path.GetExtension(file) ?? string.Empty).TrimStart('.').ToLowerInvariant(),
                    Duration = string.Empty
                })
                .ToList();
        }
        catch
        {
            return new List<Track>();
        }
    }
}
