using BattleshipModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipService
{
    public interface IGameService
    {
    
        string FireShot(int x, int y);
        bool AreAllShipsSunk();
        List<ShipModel> GetAllShips();
        List<List<string>> GetGrid();
        void ResetGame();
    }
}
