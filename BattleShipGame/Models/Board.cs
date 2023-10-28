using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleShipGame.Models
{
    public class Board
    {
        public int CoordinateBoardX { get; set; }
        public int CoordinateBoardY { get; set; }
    
        public Board()
        {

        }

        public Board(int _CoordinateBoardX, int _CoordinateBoardY)
        {
            CoordinateBoardX = _CoordinateBoardX;
            CoordinateBoardY = _CoordinateBoardY;
        }
    }
}