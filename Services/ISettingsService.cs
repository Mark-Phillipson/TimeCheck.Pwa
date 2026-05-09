using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeCheck.Pwa.Services;

public interface ISettingsService
{
    bool IsQuiet { get; set; }
    int TimeCheckIntervalMinutes { get; set; }
    bool TimeCheckEnabled { get; set; }
    int EncouragementIntervalMin { get; set; }
    int EncouragementIntervalMax { get; set; }
    bool EncouragementEnabled { get; set; }
    List<string> EncouragementMessages { get; set; }
    
    // Jokes feature settings
    int JokeIntervalMin { get; set; }
    int JokeIntervalMax { get; set; }
    bool JokesEnabled { get; set; }
    List<string> JokeMessages { get; set; }

    ValueTask LoadAsync();
    ValueTask SaveAsync();
}
