using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipModel
{
    public class ShipModel
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public List<(int x, int y)> Coordinates { get; set; }
        public List<(int x, int y)> Hits { get; set; }

        public bool IsSunk => Coordinates.All(c => Hits.Contains(c));

        public ShipModel(string name, int size)
        {
            Name = name;
            Size = size;
            Coordinates = new List<(int, int)>();
            Hits = new List<(int, int)>();
        }
    }
}
