using Model;

namespace Repository.Interfaces;

public interface IClubRepository : IRepository<Club>
{
    Task<bool> ExistsByNameAsync(string name);
    Task<Club> GetWithMembersAsync(Guid id);
}