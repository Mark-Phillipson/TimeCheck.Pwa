using System.Threading.Tasks;

namespace TimeCheck.Pwa.Services;

public interface ITtsService
{
    ValueTask SpeakAsync(string text, bool cancelPrior = true);
    ValueTask CancelAsync();
    ValueTask<bool> IsSupportedAsync();
}
