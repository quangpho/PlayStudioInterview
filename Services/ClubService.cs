using Model;
using Repository.Interfaces;

namespace Services;

public class ClubService : IClubService
{
    private readonly IClubRepository _clubRepository;

    public ClubService(IClubRepository clubRepository)
    {
        _clubRepository = clubRepository;
    }

    public async Task<bool> ClubExistsByNameAsync(string clubName)
    {
        return await _clubRepository.ExistsByNameAsync(clubName);
    }
    
    public async Task<Club> CreateClubAsync(string clubName)
    {
        var club = new Club
        {
            Id = Guid.NewGuid(),
            Name = clubName,
            Members = new List<Player>()
        };

        await _clubRepository.AddAsync(club);
        return club;
    }

    public async Task AddMemberToClubAsync(Player player, Club club)
    {
        club.Members.Add(player);
        await _clubRepository.UpdateAsync(club);
    }

    public async Task<Club> GetClubInfo(Guid id)
    {
        return await _clubRepository.GetWithMembersAsync(id);
    }
}