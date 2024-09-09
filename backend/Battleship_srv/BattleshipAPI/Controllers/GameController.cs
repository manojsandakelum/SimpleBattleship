using BattleshipModel;
using BattleshipService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService = null;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // Endpoint to fire a shot
        [HttpPost("fire")]
        public IActionResult Fire([FromBody] FireShotModel request)
        {
            try
            {
                var result = _gameService.FireShot(request.X, request.Y);
                if (_gameService.AreAllShipsSunk())
                {
                    return Ok(new { message = result, gameStatus = "All ships are sunk! You win!" });
                }

                return Ok(new { message = result, gameStatus = "Game in progress" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    error = "Invalid input",
                    details = ex.Message
                });
            }
            catch (Exception ex)
            {
                // General error handling
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred", details = ex.Message });
            }
        }

        // Endpoint to get the current game state (for UI rendering)
        [HttpGet("grid")]
        public IActionResult GetGrid()
        {
            try
            {
                var grid = _gameService.GetGrid();
                return Ok(grid);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred", details = ex.Message });
            }

        }

        // Endpoint to reset the game
        [HttpPost("reset")]
        public IActionResult ResetGame()
        {
            try
            {
                _gameService.ResetGame();
                return Ok();

            }
            catch (Exception ex)
            {
                // General error handling
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    error = "An error occurred",
                    details = ex.Message
                });
            }
        }
    }
}
