using Model;

namespace Services;

public interface IClubService
{
    Task<bool> ClubExistsByNameAsync(string clubName);
    Task<Club> CreateClubAsync(string clubName);
    Task AddMemberToClubAsync(Player player, Club club);
    Task<Club> GetClubInfo(Guid id);
}