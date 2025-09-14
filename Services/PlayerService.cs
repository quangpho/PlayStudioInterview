using Model;
using Repository.Interfaces;

namespace Services;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;

    public PlayerService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<Player> CreatePlayerAsync(long id)
    {
        var player = new Player()
        {
            PlayerId = id
        };

        await _playerRepository.AddAsync(player);
        return player;
    }
    
    public async Task<bool> HasClub(long id)
    {
        var player = await GetPlayerAsync(id);
        if (player == null)
        {
            return false;
        }

        return player.Club != null;
    }
    
    public async Task<Player> GetPlayerAsync(long id)
    {
        return await _playerRepository.GetPlayerInfo(id);
    }
}