using BattleshipModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipService
{
    public class GameService : IGameService
    {
        
        private int GridSize => 10;
        public List<ShipModel> Ships { get; set; }
        public List<(int x, int y)> ShotsFired { get; set; }

       

        public GameService()
        {
            InitializeGame();
        }

        // Randomly place ships on the grid
        private void InitializeGame()
        {
            ShotsFired = new List<(int, int)>();

            Ships = new List<ShipModel>
        {
            new ShipModel("Battleship", 5),
            new ShipModel("Destroyer1", 4),
            new ShipModel("Destroyer2", 4)
        };

            foreach (var ship in Ships)
            {
                PlaceShipRandomly(ship);
            }
        }

        // Random ship placement
        private void PlaceShipRandomly(ShipModel ship)
        {
            var random = new Random();
            bool isPlaced = false;

            while (!isPlaced)
            {
                bool isHorizontal = random.Next(2) == 0;
                int x = random.Next(0, GridSize);
                int y = random.Next(0, GridSize);

                var proposedCoordinates = new List<(int, int)>();

                for (int i = 0; i < ship.Size; i++)
                {
                    int newX = isHorizontal ? x + i : x;
                    int newY = isHorizontal ? y : y + i;

                    if (newX >= GridSize || newY >= GridSize)
                    {
                        break;
                    }

                    proposedCoordinates.Add((newX, newY));
                }

                // Check for overlaps
                if (proposedCoordinates.Count == ship.Size && !proposedCoordinates.Any(c => Ships.Any(s => s.Coordinates.Contains(c))))
                {
                    ship.Coordinates.AddRange(proposedCoordinates);
                    isPlaced = true;
                }
            }
        }

        // Check if a shot hits a ship
        public string FireShot(int x, int y)
        {
            if (ShotsFired.Contains((x, y)))
            {
                return "This position has already been targeted!";
            }

            ShotsFired.Add((x, y));

            foreach (var ship in Ships)
            {
                if (ship.Coordinates.Contains((x, y)))
                {
                    ship.Hits.Add((x, y));

                    if (ship.IsSunk)
                    {
                        return $"{ship.Name} has been sunk!";
                    }
                    return "Hit!";
                }
            }

            return "Miss!";
        }

        public List<List<string>> GetGrid()
        {
            var grid = new string[GridSize, GridSize];

            foreach (var ship in Ships)
            {
                foreach (var coord in ship.Coordinates)
                {
                    grid[coord.x, coord.y] = ShotsFired.Contains(coord) ? "H" : " ";
                }
            }

            foreach (var shot in ShotsFired)
            {
                if (grid[shot.x, shot.y] == null)
                {
                    grid[shot.x, shot.y] = "M"; // Miss
                }
            }

            // Convert the 2D array to a list of lists
            var gridList = new List<List<string>>();
            for (int i = 0; i < GridSize; i++)
            {
                var row = new List<string>();
                for (int j = 0; j < GridSize; j++)
                {
                    row.Add(grid[i, j] ?? " "); // Convert null to empty space
                }
                gridList.Add(row);
            }

            return gridList;
        }

        // Check if all ships are sunk
        public bool AreAllShipsSunk()
        {
            return Ships.All(ship => ship.IsSunk);
        }
        public List<ShipModel> GetAllShips()
        {
            return Ships;
        }
        public void ResetGame()
        {
            InitializeGame();
        }
    }
}
