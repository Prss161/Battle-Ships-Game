using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleShipGame.Models;

namespace BattleShipGame.interfaces
{
    public interface IShipPlacer
    {
        void PlaceShipOnBoard(Ships ship, Random random, bool[,] squaresOccupied);

        void SetDirectionProperties(Ships ship, int direction);
    }
   
}