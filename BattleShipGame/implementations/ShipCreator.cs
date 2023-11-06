using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleShipGame.Models;
using BattleShipGame.interfaces;

namespace BattleShipGame.implementations
{
    public class ShipCreator : IShipCreator
    {
        public void AddShipToList(List<Ships> playerShips)
        {
            Ships Carrier = new Ships("Carrier", 5, 0, 0, 0, 0, false, false, false, false);
            Ships BattleShip = new Ships("BattleShip", 4, 0, 0, 0, 0, false, false, false, false);
            Ships Destroyer = new Ships("Destroyer", 3, 0, 0, 0, 0, false, false, false, false);
            Ships Submarine = new Ships("Submarine", 3, 0, 0, 0, 0, false, false, false, false);
            Ships Patrol_boat = new Ships("Patrol_boat", 2, 0, 0, 0, 0, false, false, false, false);

            playerShips.Add(Carrier);
            playerShips.Add(BattleShip);
            playerShips.Add(Destroyer);
            playerShips.Add(Submarine);
            playerShips.Add(Patrol_boat);
        }
    }
}