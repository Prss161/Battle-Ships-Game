using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleShipGame.Models;

namespace BattleShipGame.interfaces
{
    public interface IShipCreator
    {
        public void AddShipToList(List<Ships> playerShips);
    }
}