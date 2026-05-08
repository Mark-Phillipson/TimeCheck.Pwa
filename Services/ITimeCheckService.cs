using System.Threading.Tasks;

namespace TimeCheck.Pwa.Services;

public interface ITimeCheckService
{
    Task StartAsync();
    Task StopAsync();
    ValueTask SayTimeNowAsync();
}
