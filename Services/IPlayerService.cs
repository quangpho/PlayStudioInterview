using Model;

namespace Services;

public interface IPlayerService
{
    Task<Player> GetPlayerAsync(long id);
    Task<bool> HasClub(long id);
    Task<Player> CreatePlayerAsync(long id);
}