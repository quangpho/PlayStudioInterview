using Model;

namespace Repository.Interfaces;

public interface IPlayerRepository : IRepository<Player>
{
    Task<Player> CreateIfNotExistsAsync(long id);
    Task<Player> GetPlayerInfo(long id);
}