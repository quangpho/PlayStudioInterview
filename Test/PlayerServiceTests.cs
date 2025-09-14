using Model;
using Moq;
using Repository.Interfaces;
using Services;
using Xunit;

namespace Test
{
    public class PlayerServiceTests
    {
        private readonly Mock<IPlayerRepository> _mockPlayerRepository;
        private readonly PlayerService _playerService;

        public PlayerServiceTests()
        {
            _mockPlayerRepository = new Mock<IPlayerRepository>();
            _playerService = new PlayerService(_mockPlayerRepository.Object);
        }

        [Fact]
        public async Task CreatePlayerAsync_ShouldCreatePlayerSuccessfully()
        {
            // Arrange
            long playerId = 123;
            _mockPlayerRepository.Setup(repo => repo.AddAsync(It.IsAny<Player>()))
                .Returns(Task.CompletedTask);

            // Act
            var player = await _playerService.CreatePlayerAsync(playerId);

            // Assert
            Assert.NotNull(player);
            Assert.Equal(playerId, player.PlayerId);
            _mockPlayerRepository.Verify(repo => repo.AddAsync(It.IsAny<Player>()), Times.Once);
        }

        [Fact]
        public async Task HasClub_ShouldReturnTrue_WhenPlayerHasClub()
        {
            // Arrange
            long playerId = 123;
            var player = new Player
            {
                PlayerId = playerId,
                Club = new Club { Id = System.Guid.NewGuid(), Name = "Test Club" }
            };

            _mockPlayerRepository.Setup(repo => repo.GetPlayerInfo(playerId))
                .ReturnsAsync(player);

            // Act
            var result = await _playerService.HasClub(playerId);

            // Assert
            Assert.True(result);
            _mockPlayerRepository.Verify(repo => repo.GetPlayerInfo(playerId), Times.Once);
        }

        [Fact]
        public async Task GetPlayerAsync_ShouldReturnPlayer_WhenPlayerExists()
        {
            // Arrange
            long playerId = 123;
            var expectedPlayer = new Player { PlayerId = playerId };

            _mockPlayerRepository.Setup(repo => repo.GetPlayerInfo(playerId))
                .ReturnsAsync(expectedPlayer);

            // Act
            var result = await _playerService.GetPlayerAsync(playerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(playerId, result.PlayerId);
            _mockPlayerRepository.Verify(repo => repo.GetPlayerInfo(playerId), Times.Once);
        }

        [Fact]
        public async Task GetPlayerAsync_ShouldReturnNull_WhenPlayerDoesNotExist()
        {
            // Arrange
            long playerId = 123;

            _mockPlayerRepository.Setup(repo => repo.GetPlayerInfo(playerId))
                .ReturnsAsync((Player)null);

            // Act
            var result = await _playerService.GetPlayerAsync(playerId);

            // Assert
            Assert.Null(result);
            _mockPlayerRepository.Verify(repo => repo.GetPlayerInfo(playerId), Times.Once);
        }
    }
}