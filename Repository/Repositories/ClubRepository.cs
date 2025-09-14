using Database;
using Microsoft.EntityFrameworkCore;
using Model;
using Repository.Interfaces;

namespace Repository.Repositories;

public class ClubRepository : Repository<Club>, IClubRepository
{
    public ClubRepository(ClubsDbContext context) : base(context) { }

    public async Task<bool> ExistsByNameAsync(string name)
        => await DbSet.AnyAsync(c => c.Name == name);

    public async Task<Club> GetWithMembersAsync(Guid id)
        => await DbSet
            .Include(c => c.Members)
            .FirstOrDefaultAsync(c => c.Id == id);
}