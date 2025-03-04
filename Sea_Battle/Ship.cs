using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    public class Ship
    {
        public List<(int x, int y)> Positions { get; private set; }
        public bool IsSunk { get; set; }

        public Ship(List<(int x, int y)> positions)
        {
            Positions = positions;
            IsSunk = false;
        }

        public void CheckIfSunk(int[,] playerMap)
        {
            IsSunk = Positions.All(pos => playerMap[pos.y, pos.x] == -1);
        }
    }
}
