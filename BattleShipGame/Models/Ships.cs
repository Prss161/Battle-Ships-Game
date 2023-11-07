// Class ship will contain basic parameters of a ship.
using BattleShipGame.enums;

namespace BattleShipGame.Models
{
    public class Ships
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }
        public ShipDirection Direction {get; set; }
        public int CalculatedX { get; set; }
        public int CalculatedY { get; set; }
        
        public Ships(string _Name, int _Size, int _LocationX, int _LocationY, int _CalculatedX, int _CalculatedY, ShipDirection _Direction)
        {
            Name = _Name;
            Size = _Size;
            LocationX = _LocationX;
            LocationY = _LocationY;
            CalculatedX = _CalculatedX;
            CalculatedY = _CalculatedY;
            Direction = _Direction;
        }
    }
}