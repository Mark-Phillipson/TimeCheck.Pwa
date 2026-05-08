using System.Threading.Tasks;

namespace TimeCheck.Pwa.Services;

public interface IEncouragementService
{
    Task StartAsync();
    Task StopAsync();
    ValueTask SayEncouragementNowAsync();
}
