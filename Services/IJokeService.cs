using System.Threading.Tasks;

namespace TimeCheck.Pwa.Services;

public interface IJokeService
{
    Task StartAsync();
    Task StopAsync();
    ValueTask SayJokeNowAsync();
}
