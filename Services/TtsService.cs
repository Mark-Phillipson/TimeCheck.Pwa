using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace TimeCheck.Pwa.Services;

public class TtsService : ITtsService
{
    private readonly IJSRuntime _js;
    private readonly ISettingsService _settings;

    public TtsService(IJSRuntime js, ISettingsService settings)
    {
        _js = js;
        _settings = settings;
    }

    public ValueTask SpeakAsync(string text, bool cancelPrior = true)
        => _js.InvokeVoidAsync("timecheck.speak", text ?? string.Empty, cancelPrior, _settings.Voice);

    public ValueTask CancelAsync()
        => _js.InvokeVoidAsync("timecheck.cancel");

    public ValueTask<bool> IsSupportedAsync()
        => _js.InvokeAsync<bool>("timecheck.isSupported");
}
