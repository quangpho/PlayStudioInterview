using ClubApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ClubApi;

[ApiController]
[Route("api/[controller]")]
public class ClubController : ControllerBase
{
    private readonly IClubService _clubService;
    private readonly IPlayerService _playerService;
    
    public ClubController(IClubService clubService, IPlayerService playerService)
    {
        _clubService = clubService;
        _playerService = playerService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateClub([FromQuery(Name = "Player-ID")] long playerId, 
        [FromBody] CreateClubRequestDto request)
    {
        if (await _clubService.ClubExistsByNameAsync(request.Name))
        {
            return Conflict("There is already a club has the same name");
        }

        // Check if player is already in another club
        var hasClub = await _playerService.HasClub(playerId);
        if (hasClub)
        {
            return Conflict("Player already belongs to a club");
        }
        
        var club = await _clubService.CreateClubAsync(request.Name);
        var player = await _playerService.GetPlayerAsync(playerId);
        if (player == null)
        {
            player = await _playerService.CreatePlayerAsync(playerId);
        }
        
        await _clubService.AddMemberToClubAsync(player, club);
        
        return CreatedAtAction(
            nameof(CreateClub),
            new { clubId = club.Id },
            new { id = club.Id, members = club.Members.Select(x => x.PlayerId).ToList() }
        );
    }
    
    [HttpGet("{clubId}")]
    public async Task<IActionResult> GetClub(Guid clubId)
    {
        var club = await _clubService.GetClubInfo(clubId);
        if (club == null)
        {
            return NotFound();
        }
        
        return Ok(new { id = club.Id, members = club.Members.Select(x => x.PlayerId).ToList() });
    }
    
    [HttpPost("{clubId}/members")]
    public async Task<IActionResult> AddMember(Guid clubId, 
        [FromHeader(Name = "Player-ID")] long playerId,
        [FromBody] AddMemberRequest request)
    {
        var club = await _clubService.GetClubInfo(clubId);
        if (club == null)
        {
            return NotFound();
        }
            
        // Check if player is already in another club
        var hasClub = await _playerService.HasClub(playerId);
        if (hasClub)
        {
            return Conflict("Player already belongs to a club");
        }
            
        var player = await _playerService.GetPlayerAsync(playerId);
        if (player == null)
        {
            player = await _playerService.CreatePlayerAsync(playerId);
        }
        
        await _clubService.AddMemberToClubAsync(player, club);
        
        return NoContent();
    }
}