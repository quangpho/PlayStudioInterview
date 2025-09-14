using Database;
using Microsoft.EntityFrameworkCore;
using Model;
using Repository.Interfaces;

namespace Repository.Repositories;

public class PlayerRepository : Repository<Player>, IPlayerRepository
{
    public PlayerRepository(ClubsDbContext context) : base(context)
    {
    }

    public async Task<Player> CreateIfNotExistsAsync(long id)
    {
        var player = await DbSet.Include(p => p.Club).FirstOrDefaultAsync(p => p.PlayerId == id);
        if (player == null)
        {
            player = new Player { PlayerId = id };
            await AddAsync(player);
        }
        return player;
    }
    
    public async Task<Player> GetPlayerInfo(long id)
    {
        return await DbSet.Include(p => p.Club).FirstOrDefaultAsync(p => p.PlayerId == id);
    }
}