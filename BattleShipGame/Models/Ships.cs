// Class ship will contain basic parameters of a ship.


using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace BattleShipGame.Models
{
    public class Ships
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }
        public bool DirectionNorth { get; set; }
        public bool DirectionSouth { get; set; }
        public bool DirectionEast { get; set; }
        public bool DirectionWest { get; set; }


        public Ships()
        {

        }

        public Ships(string _Name, int _Size, int _LocationX, int _LocationY, bool _DirectionNorth, bool _DirectionSouth, bool _DirectionEast, bool _DirectionWest)
        {
            Name = _Name;
            Size = _Size;
            LocationX = _LocationX;
            LocationY = _LocationY;
            DirectionNorth = _DirectionNorth;
            DirectionSouth = _DirectionSouth;
            DirectionEast = _DirectionEast;
            DirectionWest = _DirectionWest;
        }
    }
}