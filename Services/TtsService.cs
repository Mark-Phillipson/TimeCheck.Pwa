using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace TimeCheck.Pwa.Services;

public class TtsService : ITtsService
{
    private readonly IJSRuntime _js;

    public TtsService(IJSRuntime js)
    {
        _js = js;
    }

    public ValueTask SpeakAsync(string text, bool cancelPrior = true)
        => _js.InvokeVoidAsync("timecheck.speak", text ?? string.Empty, cancelPrior);

    public ValueTask CancelAsync()
        => _js.InvokeVoidAsync("timecheck.cancel");

    public ValueTask<bool> IsSupportedAsync()
        => _js.InvokeAsync<bool>("timecheck.isSupported");
}
