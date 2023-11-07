using BattleShipGame.Models;
using BattleShipGame.interfaces;
using BattleShipGame.enums;

namespace BattleShipGame.implementations
{
    public class ShipCreator : IShipCreator
    {
        public void AddShipToList(List<Ships> playerShips)
        {
            Ships Carrier = new Ships("Carrier", 5, 0, 0, 0, 0, ShipDirection.North);
            Ships BattleShip = new Ships("BattleShip", 4, 0, 0, 0, 0, ShipDirection.North);
            Ships Destroyer = new Ships("Destroyer", 3, 0, 0, 0, 0, ShipDirection.North);
            Ships Submarine = new Ships("Submarine", 3, 0, 0, 0, 0, ShipDirection.North);
            Ships Patrol_boat = new Ships("Patrol_boat", 2, 0, 0, 0, 0, ShipDirection.North);

            playerShips.Add(Carrier);
            playerShips.Add(BattleShip);
            playerShips.Add(Destroyer);
            playerShips.Add(Submarine);
            playerShips.Add(Patrol_boat);
        }
    }
}