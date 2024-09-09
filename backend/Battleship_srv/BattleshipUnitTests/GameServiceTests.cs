using BattleshipService;
using Xunit;

namespace BattleshipUnitTests
{
    public class GameServiceTests
    {
        private readonly GameService _gameService;

        public GameServiceTests()
        {
            _gameService = new GameService();
            _gameService.ResetGame(); // Initialize game state
        }

        [Fact]
        public void Test_GameInitialization_CreatesCorrectNumberOfShips()
        {
            // Act
            var ships = _gameService.Ships;

            // Assert
            Assert.Equal(3, ships.Count); // 1 Battleship and 2 Destroyers
        }

        [Fact]
        public void Test_FireShot_HitOnShip()
        {
            // Arrange
            var ship = _gameService.Ships[0];
            var target = ship.Coordinates[0]; // Get the first coordinate of the ship

            // Act
            var result = _gameService.FireShot(target.x, target.y);

            // Assert
            Assert.Contains("Hit", result);
            Assert.Contains((target.x, target.y), _gameService.ShotsFired);
        }

        [Fact]
        public void Test_FireShot_Miss()
        {
            // Act
            var result = _gameService.FireShot(0, 0); // Assuming this coordinate is empty

            // Assert
            Assert.Contains("Miss", result);
        }
    }
}

