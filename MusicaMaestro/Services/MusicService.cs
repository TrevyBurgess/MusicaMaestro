using CyberFeedForward.MusicaMaestro.Models;

namespace CyberFeedForward.MusicaMaestro.Services;

public class MusicService
{
    public List<Track> GetTracks()
    {
        return new List<Track>
        {
            new() { Title = "Piano Intro", Duration = "0:24", Instrument = "Piano" },
            new() { Title = "Bass Line", Duration = "0:48", Instrument = "Bass" },
            new() { Title = "Drum Loop", Duration = "1:12", Instrument = "Drums" },
            new() { Title = "Synth Melody", Duration = "0:36", Instrument = "Synthesizer" },
            new() { Title = "Guitar Riff", Duration = "0:52", Instrument = "Guitar" }
        };
    }
}
